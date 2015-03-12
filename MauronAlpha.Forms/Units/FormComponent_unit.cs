using System;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Units;

using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.Forms.Units {

	//Base Class for a form component
	public abstract class FormComponent_unit : Layout2d_unit, 
	I_formComponent,
	I_layoutUnit,
	IEquatable<FormComponent_unit> {

		//Constructor
		public FormComponent_unit( Layout2d_unitType unitType ):base(unitType) {}

		//Booleans
		public virtual bool Equals(FormComponent_unit other) {
			return Id.Equals(other.Id);
		}

		//EventHandlers
		public virtual bool EVENT_keyUp(KeyPress key){
			return true;
		}

	}

}
