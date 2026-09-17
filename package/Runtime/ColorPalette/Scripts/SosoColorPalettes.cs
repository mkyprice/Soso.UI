using System.Collections.Generic;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [CreateAssetMenu(fileName = "Soso Color Palette", menuName = "SosoUI/Color Palette", order = 0)]
    public class SosoColorPalettes : ScriptableObject
    {
        public int ActivePaletteIndex = 0;
        public List<ColorPalette> Palettes;

        public ColorPalette GetActivePalette()
        {
            if (ActivePaletteIndex >= Palettes.Count)
            {
                Debug.LogWarning("No active palette has been added");
                return null;
            }
            return Palettes[ActivePaletteIndex];
        }
        
        private void OnValidate()
        {
            if (Palettes != null)
            {
                ActivePaletteIndex = Mathf.Clamp(ActivePaletteIndex, 0, Palettes.Count - 1);
            }
        }
    }
}