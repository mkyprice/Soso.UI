using TMPro;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMPTextPalette : SingleColorComponent
    {
        private TMP_Text _text;
        
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