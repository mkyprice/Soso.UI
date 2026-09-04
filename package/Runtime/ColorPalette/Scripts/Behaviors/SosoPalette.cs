using UnityEngine;

namespace Soso.UI.ColorPalette
{
    public class SosoPalette : MonoBehaviour
    {
        [SerializeField] private string _colorGuid;
        [SerializeField] public SosoColorPalette Palette;

        public PaletteColor Color
        {
            get
            {
                PaletteColor color = null;
                if (Palette != null && Palette.Colors != null)
                {
                    foreach (var c in Palette.Colors)
                    {
                        if (c.Guid == _colorGuid)
                        {
                            color = c;
                            break;
                        }
                    }
                }
                return color;
            }
            set
            {
                _colorGuid = value.Guid;
                Refresh();
            }
        }

        protected virtual void Awake()
        {
            Refresh();
        }

        protected virtual void OnValidate()
        {
            Refresh();
        }

        public virtual void Refresh()
        {
            var color = GetColor();
            SetColor(color);
        }

        public Color GetColor()
        {
            if (Palette == null)
            {
                var palettes = Resources.FindObjectsOfTypeAll<SosoColorPalette>();
                if (palettes.Length > 0)
                {
                    Palette = palettes[0];
                }
            }
            
            if (Palette == null || Palette.Colors == null || Palette.Colors.Count == 0)
            {
                return UnityEngine.Color.white;
            }
            
            int index = Palette.Colors.FindIndex(color => color.Guid == _colorGuid);
            if (index == -1)
            {
                return UnityEngine.Color.white;
            }
            
            return Palette.Colors[index].Color;
        }

        protected virtual void SetColor(Color color)
        {
        }
    }
}