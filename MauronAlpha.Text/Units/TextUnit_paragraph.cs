using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Units {

	public class TextUnit_paragraph:TextComponent_unit,
	I_textUnit {

		//constructors
		public TextUnit_paragraph():base(TextUnitType_paragraph.Instance){}

		public TextUnit_paragraph(TextUnit_text parent, bool updateDependencies):this() {
			UNIT_parent = parent;
			if(updateDependencies)
				parent.AddChild(this,false);
		}

		public TextUnit_paragraph SetParent(TextUnit_text parent, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			UNIT_parent = parent;
			if(updateDependencies)
				parent.AddChild(this,false);

			return this;
		}

		public TextUnit_paragraph AddChild(TextUnit_line unit, bool updateDependencies) {
			if(IsReadOnly)
				throw Error("Is protected!,(AddChild)",this,ErrorType_protected.Instance);
				
			DATA_children.AddValue(unit);
			if(updateDependencies)
				unit.SetParent(this,false);
			
			return this;
		}
	
	}

	public class TextUnitType_paragraph:TextUnitType {
		
		private TextUnitType_paragraph():base(){}

		public override string Name { 
			get {
				return "paragraph";
			}
		}

		public static TextUnitType_paragraph Instance { 
			get {
				return new TextUnitType_paragraph();
			}
		}

	}

}
