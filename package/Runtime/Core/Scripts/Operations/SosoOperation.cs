using Soso.UI.Core.Engine;
using System.Threading;
using UnityEngine;

namespace Soso.UI.Core.Operations
{
	public abstract class SosoOperation : ISosoOperation
	{
		public SosoAwaitable Operation => _op;
		private readonly SosoAwaitable _op;

		protected SosoOperation()
		{
			_op = new SosoAwaitable(this);
		}
		public Awaitable GetAwaiter(CancellationToken token = default(CancellationToken))
		{
			return _op.GetAwaiter(token);
		}
		public SosoOperation Then(SosoOperation operation)
		{
			Operation.Then(operation.Operation);
			return operation;
		}
		
		public abstract void Start();
		public abstract void Update();
		public virtual void Finish()
		{
		}

		protected void SetCanceled()
		{
			_op.SetCanceled();
		}
		protected void SetFinished()
		{
			_op.SetFinished();
		}

		public static implicit operator SosoAwaitable(SosoOperation operation) => operation.Operation;
	}
}
