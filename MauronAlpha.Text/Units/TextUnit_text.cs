using System;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;

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
			if(updateDependencies){
				unit.SetParent(this, false);
				unit.SetContext(Context.Instance.Add(ChildCount,0,0,0));
			}

			return this;
		}

		//Context
		public override TextContext Context {
			get {
				if(DATA_context==null)
					DATA_context = new TextContext(Id,0,0,0,0);
				return DATA_context.SetIsReadOnly(true);
			}
		}
		public override TextContext CountAsContext {
			get {
				if(IsEmpty)
					return new TextContext();
				TextContext result = new TextContext(ChildCount,0,0,0);
				foreach(I_textUnit unit in DATA_children)
					result.Add(unit.CountAsContext);
				return result.SetIsReadOnly(true);
			}
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
