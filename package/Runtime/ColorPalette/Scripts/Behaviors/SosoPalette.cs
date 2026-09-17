using UnityEngine;

namespace Soso.UI.ColorPalette
{
    public class SosoPalette : MonoBehaviour
    {
        [SerializeField] private int _colorIndex;
        [SerializeField] public SosoColorPalettes palettes;

        public Color Color
        {
            get
            {
                Color color = Color.hotPink;
                ColorPalette activePalette = palettes?.GetActivePalette();
                int index = _colorIndex;
                if (activePalette != null && index >= 0 && index < activePalette.Colors.Count)
                {
                    color = activePalette.Colors[index];
                }
                return color;
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
            if (palettes == null)
            {
                var palettes = Resources.FindObjectsOfTypeAll<SosoColorPalettes>();
                if (palettes.Length > 0)
                {
                    this.palettes = palettes[0];
                }
            }

            var myPalette = palettes.GetActivePalette();
            
            if (myPalette == null || myPalette.Colors == null || myPalette.Colors.Count == 0)
            {
                Debug.LogWarning("No active palette found.");
                return Color.white;
            }

            int index = _colorIndex;

            if (index < 0 || index >= myPalette.Colors.Count)
            {
                Debug.LogWarning($"Color index {index} out of range");
                return Color.white;
            }
            
            return myPalette.Colors[index];
        }

        protected virtual void SetColor(Color color)
        {
        }
    }
}