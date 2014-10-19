using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Context {
	
	public class TextContextResult:MauronCode_textComponent {
		
		//DataTrees
		private MauronCode_dataTree<string, int> TREE_index=new MauronCode_dataTree<string, int>(SharedDataKeys);
		//DataKeys
		public static string[] SharedDataKeys=new string[] { "line", "word", "character" };

		//constructor
		private TextContextResult(){}
		public TextContextResult(MauronCode_dataTree<string,int> values){
			if(!values.Keys.Equals(SharedDataKeys)) {}
		}

		//Line,Word,Character index
		public int Line {
			get {
				if(!TREE_index.IsSet("line")){
					NullError("Character can not be null!",this,typeof(int));
				}
				return TREE_index.Value("line");
			}
		}
		public int Word {
			get {
				if( !TREE_index.IsSet("word") ) {
					NullError("Character can not be null!", this, typeof(int));
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

		#region Boolean States
		//Check if a context "Fits" into a textcomponent
		public bool Fits(TextUnit_text text){
			return text.Count.Fits(this);
		}
		#endregion
	}

}
