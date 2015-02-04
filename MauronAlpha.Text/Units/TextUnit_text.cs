using System;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;


namespace MauronAlpha.Text.Units {

	//Base class of a text
	public class TextUnit_text:TextComponent_unit,
	I_textUnit {
		
		//constructor
		public TextUnit_text():base(TextUnitType_text.Instance) {}

		//Methods
		public TextUnit_text SetText(string text) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetText)", this, ErrorType_protected.Instance);
			Encoding.SetTextToString(this,text);
			
			return this;
		}
		public TextUnit_text AddChild(TextUnit_paragraph unit, bool updateDependencies) {
			if(IsReadOnly)
				throw Error("Is protected!,(AddChild)",this,ErrorType_protected.Instance);
			DATA_children.AddValue(unit);
			if(updateDependencies)
				unit.SetParent(this, false);

			return this;
		}

	}

	public class TextUnitType_text:TextUnitType,
	I_textUnitType {
		
		public override string Name { 
			get { 
				return "text"; 
			} 
		}

		public override bool CanHaveParent {
			get { 
				return false; 
			}
		}
	
		public static TextUnitType_text Instance { 
			get {
				return new TextUnitType_text();
			} 
		}
	}
}
