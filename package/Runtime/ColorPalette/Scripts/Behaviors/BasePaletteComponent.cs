using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
	public abstract class BasePaletteComponent : MonoBehaviour, IPaletteComponent
	{
		public SosoColorPalettes ColorPalette
		{
			get => _colorPalette; 
			set => _colorPalette = value;
		}
		[SerializeField] private SosoColorPalettes _colorPalette;
		
		protected virtual void Awake()
		{
			Refresh();
		}

		protected virtual void OnValidate()
		{
			IPaletteComponent.Validate(this);
			Refresh();
		}
		
		public abstract IEnumerable<SosoColor> GetColors();
		
		public virtual void Refresh()
		{
			SetColor(GetColor());
		}

		public abstract Color GetColor();

		public abstract void SetColor(Color color);
	}
}
