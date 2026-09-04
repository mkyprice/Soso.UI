using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(Image))]
    public class ImagePalette : SosoPalette
    {
        private Image _image;
        
        protected override void SetColor(Color color)
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.color = color;
        }
    }
}