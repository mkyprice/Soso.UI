using System;
using UnityEngine.LowLevel;

namespace Soso.UI.Core.Helpers
{
	public static class UnityEngineHelpers
	{
		/// <summary>
		/// Insert a loop into the root system
		/// </summary>
		/// <param name="root"></param>
		/// <param name="targetType"></param>
		/// <param name="customSystem"></param>
		/// <returns></returns>
		public static bool InsertLoopSystem(ref PlayerLoopSystem root, Type targetType, PlayerLoopSystem customSystem)
		{
			if (root.type == targetType)
			{
				var prevSystems = root.subSystemList;
				int prevSystemsLength = prevSystems?.Length ?? 0;
				var newSystems = new PlayerLoopSystem[prevSystemsLength + 1];
				
				// Check if it's already in there
				if (prevSystems != null)
				{
					for (int i = 0; i < prevSystemsLength; i++)
					{
						if (prevSystems[i].type == customSystem.type)
						{
							return true;
						}
					}
					
					// Copy to new system array
					if (prevSystemsLength > 0)
					{
						Array.Copy(prevSystems, newSystems, prevSystemsLength);
					}
				}
				
				// Append the new system and reassign the list
				newSystems[prevSystemsLength] = customSystem;
				root.subSystemList = newSystems;
				return true;
			}

			// Recursively search children
			if (root.subSystemList != null)
			{
				for (int i = 0; i < root.subSystemList.Length; i++)
				{
					if (InsertLoopSystem(ref root.subSystemList[i], targetType, customSystem))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
