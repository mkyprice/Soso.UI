using UnityEngine;

namespace Soso.UI.ColorPalette
{
    public class RendererPalette : SingleColorComponent
    {
        private Renderer _renderer;
        
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