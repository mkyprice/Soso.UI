using System.Collections.Generic;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
	public interface IPaletteComponent
	{
		public SosoColorPalettes ColorPalette { get; set; }
		public void Refresh();
		
		public IEnumerable<SosoColor> GetColors();

		public Color GetColor();

		public void SetColor(Color color);

		public static void Validate(IPaletteComponent palette)
		{
#if UNITY_EDITOR
			if (palette.ColorPalette == false)
			{
				var palettesInProject = Resources.FindObjectsOfTypeAll<SosoColorPalettes>();
				if (palettesInProject.Length > 0)
				{
					palette.ColorPalette = palettesInProject[0];
				}
			}
			foreach (var color in palette.GetColors())
			{
				color?.SetPalettes(palette.ColorPalette);
			}
#endif
			palette.Refresh();
		}
	}
}
