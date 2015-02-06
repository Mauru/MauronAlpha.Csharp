using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	public class TextUnit_line:TextComponent_unit {

		//Constructors
		public TextUnit_line():base(TextUnitType_line.Instance) {}
		public TextUnit_line(TextUnit_paragraph parent, bool updateDependencies):this() {
			UNIT_parent = parent;
			if(updateDependencies){
				parent.AddChild(this,false);
				SetContext(parent.Context.Instance.Add(0, parent.ChildCount, 0, 0));
			}
		}
		
		//Methods
		public TextUnit_line SetParent (TextUnit_paragraph parent, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			UNIT_parent=parent;
			if( updateDependencies ){
				parent.AddChild(this, false);
				SetContext(parent.Context.Instance.Add(0, parent.ChildCount, 0, 0));
			}
			return this;
		}
		public TextUnit_line AddChild (TextUnit_word unit, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(AddChild)", this, ErrorType_protected.Instance);

			DATA_children.AddValue(unit);
			if( updateDependencies ){
				unit.SetParent(this, false);
				unit.SetContext(Context.Instance.Add(0, 0, ChildCount, 0));
			}
			return this;
		}

		//Count
		public override TextContext CountAsContext {
			get {
				if( IsEmpty )
					return new TextContext();
				TextContext result = new TextContext(0, 0, ChildCount, 0);
				foreach(I_textUnit unit in DATA_children)
					result.Add(unit.CountAsContext);
				return result.SetIsReadOnly(true);
			}
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
