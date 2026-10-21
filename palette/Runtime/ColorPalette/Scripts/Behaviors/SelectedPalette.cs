using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Soso.UI.ColorPalette
{
	public class SelectedPalette : UIBehaviour, ISelectHandler, IDeselectHandler, IPaletteComponent
	{
		[SerializeField] public Graphic TargetGraphic;
		[SerializeField] public SosoColor SelectedColor;
		[SerializeField] public SosoColor DeselectedColor;
		[SerializeField] public SosoColorPalettes Palettes;
		public SosoColorPalettes ColorPalette { get => Palettes; set => Palettes = value; }
		
		private bool _isSelected;

		protected override void OnValidate()
		{
			base.OnValidate();
			IPaletteComponent.Validate(this);
			Refresh();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			_isSelected = false;
			Refresh();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_isSelected = false;
		}

		public void OnSelect(BaseEventData eventData)
		{
			_isSelected = true;
			Refresh();
		}
		public void OnDeselect(BaseEventData eventData)
		{
			_isSelected = false;
			Refresh();
		}
		
		public void Refresh()
		{
			if (TargetGraphic == false)
			{
				return;
			}
			if (_isSelected)
			{
				TargetGraphic.color = SelectedColor.GetColor();
			}
			else
			{
				TargetGraphic.color = DeselectedColor.GetColor();
			}
		}
		
		public IEnumerable<SosoColor> GetColors()
		{
			yield return SelectedColor;
			yield return DeselectedColor;
		}
	}
}
