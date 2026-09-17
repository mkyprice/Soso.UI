using System;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [Serializable]
    public class SosoColor
    {
        [SerializeField, HideInInspector] private int _colorIndex;
        [SerializeField, HideInInspector] private SosoColorPalettes _palettes;

        public SosoColor()
        {
        }

        public SosoColor(SosoColorPalettes palettes)
        {
            _palettes = palettes;
        }

        public void SetPalettes(SosoColorPalettes palettes)
        {
            _palettes = palettes;
        }

        public ColorPalette GetActivePalette()
        {
            if (_palettes == false || _palettes.Palettes?.Count <= 0)
            {
                Debug.LogWarning("No active palette found.");
                return null;
            }
            return _palettes.GetActivePalette();
        }

        public Color GetColor()
        {
            if (_palettes == null)
            {
                Debug.LogWarning("No palette assigned.");
                return Color.hotPink;
            }

            var myPalette = _palettes.GetActivePalette();
            
            if (myPalette == null || myPalette.Colors == null || myPalette.Colors.Count == 0)
            {
                Debug.LogWarning("No active palette found.");
                return Color.hotPink;
            }

            int index = _colorIndex;

            if (index < 0 || index >= myPalette.Colors.Count)
            {
                Debug.LogWarning($"Color index {index} out of range");
                return Color.hotPink;
            }
            
            return myPalette.Colors[index];
        }
    }
}