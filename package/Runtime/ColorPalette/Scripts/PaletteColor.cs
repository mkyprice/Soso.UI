using System;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [Serializable]
    public class PaletteColor
    {
        public string Name;
        public Color Color;
        public string Guid;

        public PaletteColor()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
    }
}