using System;
using System.Collections.Generic;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [Serializable]
    public class ColorPalette
    {
        [SerializeField] public string Name;
        [SerializeField] public List<Color> Colors;
    }
}