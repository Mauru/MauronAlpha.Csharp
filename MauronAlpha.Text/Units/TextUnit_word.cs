using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Units {
	
	public class TextUnit_word:TextComponent_unit {

		//Constructors
		public TextUnit_word():base(TextUnitType_word.Instance) {}
		public TextUnit_word(TextUnit_line parent, bool updateDependencies):this() {
			UNIT_parent = parent;
			if(updateDependencies)
				parent.AddChild(this,false);
		}

		public TextUnit_word SetParent (TextUnit_line parent, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			UNIT_parent=parent;
			if( updateDependencies )
				parent.AddChild(this, false);

			return this;
		}

		public TextUnit_word AddChild (TextUnit_character unit, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(AddChild)", this, ErrorType_protected.Instance);

			DATA_children.AddValue(unit);
			if( updateDependencies )
				unit.SetParent(this, false);

			return this;
		}


	}

	public class TextUnitType_word : TextUnitType {
		
		public override string Name {
			get {
				return "word";
			}
		}

		public static TextUnitType_word Instance {
			get {
				return new TextUnitType_word();
			}
		}
	
	}

}
