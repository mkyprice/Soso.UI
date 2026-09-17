using System.Collections.Generic;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    public class RendererPalette : BasePaletteComponent
    {
        [SerializeField] public SosoColor color;
        private Renderer _renderer;

        public override IEnumerable<SosoColor> GetColors()
        {
            yield return color;
        }
        public override Color GetColor()
        {
            return color.GetColor();
        }
        
        public override void SetColor(Color color)
        {
            if (_renderer == false)
            {
                _renderer = GetComponent<Renderer>();
            }

            if (_renderer != null)
            {
                _renderer.material.color = color;
            }
        }
    }
}