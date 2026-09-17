using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(Image))]
    public class ImagePalette : BasePaletteComponent
    {
        [SerializeField] public SosoColor color;
        
        private Image _image;

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
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.color = color;
        }
    }
}