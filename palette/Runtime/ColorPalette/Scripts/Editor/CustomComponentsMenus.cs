using Soso.UI.Core.Behaviors;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette.Editor
{
	public class CustomComponentsMenus
	{
		[MenuItem("GameObject/UI (Canvas)/Soso Paletted Button", false, 11)]
		private static void CreateCustomGameObject(MenuCommand menuCommand)
		{
			GameObject go = new GameObject("Paletted Button", 
				typeof(CanvasRenderer), typeof(Image), typeof(PalettedButton)
				);
			
			// Default to standard button size
			if (go.TryGetComponent(out RectTransform rectTransform))
			{
				rectTransform.sizeDelta = new Vector2(160, 30);
			}
			
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        
			// Allow undo
			Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        
			// Select
			Selection.activeObject = go;
		}
	}
}
