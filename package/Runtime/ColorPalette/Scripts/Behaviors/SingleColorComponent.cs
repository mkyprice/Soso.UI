using System.Collections.Generic;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
	public abstract class SingleColorComponent : MonoBehaviour, IPaletteComponent
	{
		public SosoColorPalettes ColorPalette
		{
			get => _colorPalette; 
			set => _colorPalette = value;
		}
		public SosoColor Color
		{
			get
			{
				_color ??= new SosoColor(ColorPalette);
				return _color;
			}
		}

		[SerializeField] private SosoColorPalettes _colorPalette;
		[SerializeField] private SosoColor _color;
		
		protected virtual void Awake()
		{
			Refresh();
		}

		protected virtual void OnValidate()
		{
			IPaletteComponent.Validate(this);
			Refresh();
		}

		public IEnumerable<SosoColor> GetColors()
		{
			yield return Color;
		}
		
		public virtual void Refresh()
		{
			SetColor(GetColor());
		}

		public Color GetColor()
		{
			return Color.GetColor();
		}

		public abstract void SetColor(Color color);
	}
}
