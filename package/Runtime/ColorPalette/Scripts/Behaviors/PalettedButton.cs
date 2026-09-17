using Soso.UI.ColorPalette.Events;
using Soso.UI.Core.Types;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
	[RequireComponent(typeof(Image))]
	public class PalettedButton : Button, IPaletteComponent
	{
		[SerializeField] public SosoColor NormalColor;
		[SerializeField] public SosoColor HighlightedColor;
		[SerializeField] public SosoColor DisabledColor;
		[SerializeField] public SosoColor PressedColor;
		[SerializeField] public SosoColor SelectedColor;
		[SerializeField] private SosoColorPalettes _colorPalette;
		[SerializeField] public ButtonStateChangedEvent StateChanged;
		public SosoColorPalettes ColorPalette { get => _colorPalette; set => _colorPalette = value; }
		public SELECTION_STATE State => (SELECTION_STATE)base.currentSelectionState;
		
		protected override void Awake()
		{
			base.Awake();
			
			Refresh();
		}
		
		protected override void OnValidate()
		{
			base.OnValidate();
			
			base.transition = Transition.None;
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
		
		public Color GetColor()
		{
			switch (currentSelectionState)
			{
				case SelectionState.Normal:
					return NormalColor.GetColor();
				case SelectionState.Highlighted:
					return HighlightedColor.GetColor();
				case SelectionState.Pressed:
					return PressedColor.GetColor();
				case SelectionState.Selected:
					return SelectedColor.GetColor();
				case SelectionState.Disabled:
					return DisabledColor.GetColor();
				default:
					throw new NotImplementedException($"What the heck is state {currentSelectionState}?");
			}
		}
		
		public void SetColor(Color color)
		{
			image.color = color;
		}

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
			Refresh();
			StateChanged.Invoke(State);
		}
	}
}
