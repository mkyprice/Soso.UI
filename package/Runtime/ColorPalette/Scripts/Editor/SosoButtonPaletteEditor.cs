using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Soso.UI.ColorPalette.Editor
{
	[CustomEditor(typeof(SosoButtonPalette))]
	[CanEditMultipleObjects]
	public class SosoButtonPaletteEditor : UnityEditor.Editor
	{
		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = new VisualElement();

			// Add colors
			AddPaletteButton(root, "_normalColor");
			AddPaletteButton(root, "_highlightedColor");
			AddPaletteButton(root, "_pressedColor");
			AddPaletteButton(root, "_selectedColor");
			AddPaletteButton(root, "_disabledColor");

			// Add the palette
			root.Add(new PropertyField(serializedObject.FindProperty("_colorPalette")));
			// Refresh
			var refreshButton = new Button(() =>
				{
					foreach (var t in targets)
					{
						if (t is IPaletteComponent palette)
						{
							palette.Refresh();
						}
					}
				})
				{
					text = "Refresh"
				};
			root.Add(refreshButton);

			return root;
		}

		private void AddPaletteButton(VisualElement root, string propName)
		{
			root.Add(new PropertyField(serializedObject.FindProperty(propName)));
		}
	}
}
