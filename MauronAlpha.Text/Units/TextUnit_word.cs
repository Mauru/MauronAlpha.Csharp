using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	public class TextUnit_word:TextComponent_unit {

		//Constructors
		public TextUnit_word():base(TextUnitType_word.Instance) {}
		public TextUnit_word(TextUnit_line parent, bool updateDependencies):this() {
			UNIT_parent = parent;
			if(updateDependencies){
				parent.AddChild(this,false);
				SetContext(parent.Context.Instance.Add(0, 0, parent.ChildCount, 0));
			}
		}
		
		//Methods
		public TextUnit_word SetParent (TextUnit_line parent, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			UNIT_parent=parent;
			if( updateDependencies ){
				parent.AddChild(this, false);
				SetContext(parent.Context.Instance.Add(0, 0, parent.ChildCount, 0));
			}
			return this;
		}
		public TextUnit_word AddChild (TextUnit_character unit, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(AddChild)", this, ErrorType_protected.Instance);

			DATA_children.AddValue(unit);
			if( updateDependencies ){
				unit.SetParent(this, false);
				unit.SetContext(Context.Instance.Add(0, 0, 0, ChildCount));
			}
			return this;
		}

		//Count
		public override TextContext CountAsContext {
			get {
				return new TextContext(0,0,0,ChildCount).SetIsReadOnly(true);
			}
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
