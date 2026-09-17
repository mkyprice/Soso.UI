using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(RawImage))]
    public class RawImagePalette : SingleColorComponent
    {
        private RawImage _image;
        
        public override void SetColor(Color color)
        {
            if (_image == null)
            {
                _image = GetComponent<RawImage>();
            }
            _image.color = color;
        }
    }
}