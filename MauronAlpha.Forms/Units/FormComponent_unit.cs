using MauronAlpha.HandlingErrors;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Units;

using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.Forms.Units {

	//Base Class for a form component
	public abstract class FormComponent_unit : Layout2d_unit, 
	I_formComponent,
	I_layoutUnit {

		//Constructor
		public FormComponent_unit( Layout2d_unitType unitType ):base(unitType) {}

	}

}
