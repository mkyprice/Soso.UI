using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [CreateAssetMenu(fileName = "Soso Color Palette", menuName = "SosoUI/Color Palette", order = 0)]
    public class SosoColorPalette : ScriptableObject
    {
        public List<PaletteColor> Colors;
        
        private void OnValidate()
        {
            if (Colors != null)
            {
                string pattern = @" \(\d+\)$";
                // Replace all repeats
                for (int i = 0; i < Colors.Count; i++)
                {
                    var color = Colors[i];
                    color.Name = Regex.Replace(color.Name, pattern, string.Empty);
                    Colors[i] = color;
                }
                for (int i = Colors.Count - 1; i >= 0; i--)
                {
                    var color = Colors[i];
                    if (string.IsNullOrEmpty(color.Name))
                    {
                        color.Name = i.ToString();
                    }
                    
                    // Repeats
                    int repeats = 0;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (j == i) continue;
                        if (Colors[j].Name == color.Name) repeats++;
                    }

                    if (repeats > 0)
                    {
                        color.Name = $"{color.Name} ({repeats})";
                    }
                    Colors[i] = color;
                }
            }
        }
    }
}