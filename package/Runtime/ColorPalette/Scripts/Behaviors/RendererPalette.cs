using UnityEngine;

namespace Soso.UI.ColorPalette
{
    public class RendererPalette : SosoPalette
    {
        private Renderer _renderer;

        protected override void SetColor(Color color)
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