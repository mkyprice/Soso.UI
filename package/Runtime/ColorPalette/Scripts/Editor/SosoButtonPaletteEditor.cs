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
			AddPaletteButton(root, "_normalColor", "Normal");
			AddPaletteButton(root, "_highlightedColor", "Highlighted");
			AddPaletteButton(root, "_pressedColor", "Pressed");
			AddPaletteButton(root, "_selectedColor", "Selected");
			AddPaletteButton(root, "_disabledColor", "Disabled");

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

		private void AddPaletteButton(VisualElement root, string propName, string tooltip)
		{
			root.Add(new TextElement()
			{
				text = tooltip,
			});
			root.Add(new PropertyField(serializedObject.FindProperty(propName)));
		}
	}
}
