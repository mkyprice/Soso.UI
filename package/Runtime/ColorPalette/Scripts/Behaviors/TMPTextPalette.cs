using TMPro;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMPTextPalette : SosoPalette
    {
        private TMP_Text _text;
        protected override void SetColor(Color color)
        {
            if  (_text == null)
            {
                _text = GetComponent<TMP_Text>();
            }
            _text.color = color;
        }
    }
}