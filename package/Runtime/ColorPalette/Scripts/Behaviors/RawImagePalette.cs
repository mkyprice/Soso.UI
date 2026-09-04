using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(RawImage))]
    public class RawImagePalette : SosoPalette
    {
        private RawImage _image;
        
        protected override void SetColor(Color color)
        {
            if (_image == null)
            {
                _image = GetComponent<RawImage>();
            }
            _image.color = color;
        }
    }
}