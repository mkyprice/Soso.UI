using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(RawImage))]
    public class RawImagePalette : BasePaletteComponent
    {
        [SerializeField] public SosoColor color;
        
        private RawImage _image;
        
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
                _image = GetComponent<RawImage>();
            }
            _image.color = color;
        }
    }
}