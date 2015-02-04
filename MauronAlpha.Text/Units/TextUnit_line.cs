using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Units {
	
	public class TextUnit_line:TextComponent_unit {

		//Constructors
		public TextUnit_line():base(TextUnitType_line.Instance) {}
		public TextUnit_line(TextUnit_paragraph parent, bool updateDependencies):this() {
			UNIT_parent = parent;
			if(updateDependencies)
				parent.AddChild(this,false);
		}

		public TextUnit_line SetParent (TextUnit_paragraph parent, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			UNIT_parent=parent;
			if( updateDependencies )
				parent.AddChild(this, false);

			return this;
		}

		public TextUnit_line AddChild (TextUnit_word unit, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(AddChild)", this, ErrorType_protected.Instance);

			DATA_children.AddValue(unit);
			if( updateDependencies )
				unit.SetParent(this, false);

			return this;
		}

	}

	public class TextUnitType_line:TextUnitType {
		
		public override string Name {
			get {
				return "line";
			}
		}
	
		public static TextUnitType_line Instance {
			get {
				return new TextUnitType_line();
			}
		}
	
	}

}
