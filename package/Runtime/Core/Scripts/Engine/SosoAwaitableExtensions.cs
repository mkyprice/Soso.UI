using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Soso.UI.Core.Engine
{
	sealed public partial class SosoAwaitable
	{
		public static Awaitable RunAwaitablesAsync(params SosoAwaitable[] operations)
			=> RunAwaitablesAsync(CancellationToken.None, operations);
		
		public static async Awaitable RunAwaitablesAsync(CancellationToken token, params SosoAwaitable[] operations)
		{
			int finished = 0;
			var tcs = new AwaitableCompletionSource();
			List<Exception> exceptions = null;
            
			using CancellationTokenRegistration tokenReg = token.Register(() => tcs.TrySetCanceled());
            
			foreach (var operation in operations)
			{
				_ = RunAsync(operation);
			}
			await tcs.Awaitable;

			if (exceptions != null)
			{
				tcs.SetException(new AggregateException(exceptions));
			}
            
			async Awaitable RunAsync(SosoAwaitable operation)
			{
				try
				{
					await operation.GetAwaiter(token);
				}
				catch (Exception e)
				{
					exceptions ??= new List<Exception>();
					exceptions.Add(e);
				}
				finally
				{
					finished++;
					if (finished == operations.Length)
					{
						tcs.SetResult();
					}
				}
			}
		}
	}
}
