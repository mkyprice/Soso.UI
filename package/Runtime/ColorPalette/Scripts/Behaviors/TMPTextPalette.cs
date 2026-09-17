using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMPTextPalette : BasePaletteComponent
    {
        [SerializeField] public SosoColor color;
        
        private TMP_Text _text;
        
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
            if  (_text == null)
            {
                _text = GetComponent<TMP_Text>();
            }
            _text.color = color;
        }
    }
}