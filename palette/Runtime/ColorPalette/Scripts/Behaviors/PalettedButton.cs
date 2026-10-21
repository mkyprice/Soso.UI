using Soso.UI.Core.Behaviors;
using Soso.UI.Core.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Soso.UI.ColorPalette
{
	public class PalettedButton : SosoButton, IPaletteComponent
	{
		[SerializeField] private SosoColor _normalColor;
		[SerializeField] private SosoColor _highlightedColor;
		[SerializeField] private SosoColor _disabledColor;
		[SerializeField] private SosoColor _pressedColor;
		[SerializeField] private SosoColor _selectedColor;
		[SerializeField] private SosoColorPalettes _colorPalette;
		public SosoColorPalettes ColorPalette { get => _colorPalette; set => _colorPalette = value; }
		public SosoColor NormalColor
		{
			get
			{
				_normalColor ??= new SosoColor(_colorPalette);
				return _normalColor;
			}
		}
		public SosoColor HighlightedColor
		{
			get
			{
				_highlightedColor ??= new SosoColor(_colorPalette);
				return _highlightedColor;
			}
		}
		public SosoColor DisabledColor
		{
			get
			{
				_disabledColor ??= new SosoColor(_colorPalette);
				return _disabledColor;
			}
		}
		public SosoColor PressedColor
		{
			get
			{
				_pressedColor ??= new SosoColor(_colorPalette);
				return _pressedColor;
			}
		}
		public SosoColor SelectedColor
		{
			get
			{
				_selectedColor ??= new SosoColor(_colorPalette);
				return _selectedColor;
			}
		}
		
		protected override void OnValidate()
		{
			base.OnValidate();
			IPaletteComponent.Validate(this);
			Refresh();
		}

		public void Refresh()
		{
			SetColor(GetColor());
		}
		
		public IEnumerable<SosoColor> GetColors()
		{
			yield return NormalColor;
			yield return HighlightedColor;
			yield return DisabledColor;
			yield return PressedColor;
			yield return SelectedColor;
		}
		
		protected override void StateChanged(SELECTION_STATE arg0, bool instant)
		{
			Refresh();
		}
		
		public Color GetColor()
		{
			switch (State)
			{
				case SELECTION_STATE.Normal:
					return NormalColor.GetColor();
				case SELECTION_STATE.Highlighted:
					return HighlightedColor.GetColor();
				case SELECTION_STATE.Pressed:
					return PressedColor.GetColor();
				case SELECTION_STATE.Selected:
					return SelectedColor.GetColor();
				case SELECTION_STATE.Disabled:
					return DisabledColor.GetColor();
				default:
					throw new NotImplementedException($"What the heck is state {State}?");
			}
		}
		
		public void SetColor(Color color)
		{
			targetGraphic.color = color;
		}
	}
}
