using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Context {
	
	public class TextContextResult:TextComponent_context {
		
		//DataTrees
		private MauronCode_dataTree<string, int> TREE_index=new MauronCode_dataTree<string, int>(SharedDataKeys);
		//DataKeys
		public static string[] SharedDataKeys = new string[] { "paragraph", "line", "word", "character" };

		//constructor
		private TextContextResult(){}
		public TextContextResult(MauronCode_dataTree<string,int> values){
			if(!values.Keys.Equals(SharedDataKeys)) {
				Error("Invalid Values",this,ErrorType_constructor.Instance);
			}
			SetValue("paragraph", Paragraph);
			SetValue("line", Line);
			SetValue("word", Line);
			SetValue("character", Character);
		}

		//Line,Word,Character index
		public int Paragraph {
			get {
				if( !TREE_index.IsSet("paragraph") ) {
					NullError("Paragraph can not be null!", this, typeof(int));
				}
				return TREE_index.Value("paragraph");
			}
		}
		public int Line {
			get {
				if(!TREE_index.IsSet("line")){
					NullError("Line can not be null!",this,typeof(int));
				}
				return TREE_index.Value("line");
			}
		}
		public int Word {
			get {
				if( !TREE_index.IsSet("word") ) {
					NullError("Word can not be null!", this, typeof(int));
				}
				return TREE_index.Value("word");
			}
		}
		public int Character {
			get {
				if( !TREE_index.IsSet("character") ) {
					NullError("Line can not be null!", this, typeof(int));
				}
				return TREE_index.Value("character");
			}
		}

		//Set A property of the dataTree
		private TextContextResult SetValue(string key, int value){
			if(value < 0) {
				Error("Value must be >= 0!,{"+key+","+value+"},(SetValue)",this,ErrorType_index.Instance);
			}
			TREE_index.SetValue(key,value);
			return this;
		}

		#region Boolean States
		//Check if a context "Fits" into a textcomponent
		public bool Fits(TextUnit_text text){
			return text.Count.Fits(this);
		}
		#endregion
	}

}
