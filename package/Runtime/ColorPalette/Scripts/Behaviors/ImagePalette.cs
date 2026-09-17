using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(Image))]
    public class ImagePalette : SingleColorComponent
    {
        private Image _image;
        
        public override void SetColor(Color color)
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.color = color;
        }
    }
}