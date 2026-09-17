using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Soso.UI.ColorPalette.Editor
{
	[CustomEditor(typeof(SingleColorComponent), true), CanEditMultipleObjects]
	public class SingleColorComponentEditor : UnityEditor.Editor
	{
		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = new VisualElement();
			
			// Create default
			VisualElement defaultInspector = new VisualElement();
			InspectorElement.FillDefaultInspector(defaultInspector, serializedObject, this);
			root.Add(defaultInspector);

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
	}
}
