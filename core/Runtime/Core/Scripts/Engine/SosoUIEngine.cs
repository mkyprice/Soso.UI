using Soso.UI.Core.Helpers;
using Soso.UI.Core.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Soso.UI.Core.Engine
{
	public static class SosoUIEngine
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			_operations.Clear();

			PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();
			PlayerLoopSystem uiLoop = new PlayerLoopSystem()
			{
				type = typeof(SosoUIEngine),
				updateDelegate = UpdateTick
			};

			if (UnityEngineHelpers.InsertLoopSystem(ref playerLoop, typeof(Update), uiLoop))
			{
				PlayerLoop.SetPlayerLoop(playerLoop);
			}
			else
			{
				Debug.LogError($"[{nameof(SosoUIEngine)}] - Failed to insert update loop");
			}
		}

		private static readonly HashSet<SosoAwaitable> _operations = new HashSet<SosoAwaitable>();
		private static readonly List<SosoAwaitable> _operationIterCache = new List<SosoAwaitable>();
		
		public static void Register(SosoAwaitable operation)
		{
			_operations.Add(operation);
		}
		
		public static void Deregister(SosoAwaitable operation)
		{
			_operations.Remove(operation);
		}

		private static void UpdateTick()
		{
			_operationIterCache.Clear();
			_operationIterCache.AddRange(_operations);
			foreach (var op in _operationIterCache)
			{
				if (op.IsReady == false) continue;
				
				try
				{
					op.Update();
				}
				catch (OperationCanceledException)
				{
					_operations.Remove(op);
					Debug.Log($"[{nameof(SosoUIEngine)}] - Operation {op} cancelled");
					continue;
				}
				catch (Exception e)
				{
					_operations.Remove(op);
					op.SetException(e);
					continue;
				}

				if (op.IsFinished())
				{
					_operations.Remove(op);
				}
			}
		}
	}
}
