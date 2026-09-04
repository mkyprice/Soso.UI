using Soso.UI.Core.Operations;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Soso.UI.Core.Engine
{
    sealed public partial class SosoAwaitable
    {
        public bool IsReady
        {
            get => _parent == null || _parent.IsFinished();
        }
        private readonly ISosoOperation _operation;
        private bool _isFinished;
        private bool _isCanceled;
        private bool _isStarted;
        private AwaitableCompletionSource _awaiter;
        private SosoAwaitable _parent, _child;
        private CancellationToken _cancellationToken = CancellationToken.None;

        public SosoAwaitable(ISosoOperation operation)
        {
            _operation = operation;
            _isStarted = false;
            SosoUIEngine.Register(this);
        }

        public void Update()
        {
            _cancellationToken.ThrowIfCancellationRequested();

            if (_isStarted == false)
            {
                _isStarted = true;
                _operation.Start();
            }
            
            _operation.Update();
        }

        public SosoAwaitable Then(SosoAwaitable next)
        {
            next._parent = this;
            _child = next;
            return next;
        }

        public void SetFinished()
        {
            _isFinished = true;
            
            _awaiter?.TrySetResult();
            
            _operation.Finish();

            SosoUIEngine.Deregister(this);

            // Ensure child has starting values
            _child?._operation.Start();
        }

        public void SetException(Exception exception)
        {
            if (_awaiter != null)
            {
                _awaiter.TrySetException(exception);
            }
            else
            {
                Debug.LogException(exception);
            }
            _isFinished = true;
        }

        public void SetCanceled()
        {
            _isFinished = true;
            _isCanceled = true;
            _awaiter?.TrySetCanceled();
        }

        public bool IsFinished()
        {
            return _isFinished || _isCanceled;
        }

        public Awaitable GetAwaiter(CancellationToken token = default)
        {
            if (_awaiter != null)
            {
                return _awaiter.Awaitable;
            }
            return SetAwaiter(new AwaitableCompletionSource(), token);
        }

        private Awaitable SetAwaiter(AwaitableCompletionSource awaiter, CancellationToken token)
        {
            if (awaiter == null)
            {
                return null;
            }
            _cancellationToken = token;
            _cancellationToken.Register(SetCanceled);
            _awaiter = awaiter;
            
            // Are we already finished?
            if (IsFinished())
            {
                if (_isCanceled) _awaiter.TrySetCanceled();
                else SetFinished();
            }
            return _awaiter.Awaitable;
        }
    }
}