using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Context {
	//An object tracking how many textcomponents can be in a TextUnit
	public class TextContextLimit : TextComponent_context {

		//DataTrees
		private MauronCode_dataTree<string, int> TREE_limit=new MauronCode_dataTree<string, int>(SharedDataKeys, SharedDataDefaults);
		//DataKeys
		private static string[] SharedDataKeys=new string[] { "paragraph", "line", "word", "character" };
		//Default Values
		private static int[] SharedDataDefaults=new int[] { 0, 0, 0, 0 };

		//constructor
		private TextContextLimit ( ) { }
		public TextContextLimit (int paragraph, int line, int word, int character) {
			TREE_limit.SetValues(new int[]{
				paragraph,line, word, character
			});
		}

		//check if a result fits into a count
		public bool Fits (TextContextResult result) {
			return (
				(result.Paragraph>=0&&result.Paragraph<Paragraph)&&
				(result.Line>=0&&result.Line<Line)&&
				(result.Word>=0&&result.Word<Word)&&
				(result.Character>=0&&result.Character<Character)
			);
		}

		//get individual positions
		public int Paragraph {
			get {
				return TREE_limit.Value("paragraph");
			}
		}
		public int Line {
			get {
				return TREE_limit.Value("line");
			}
		}
		public int Word {
			get {
				return TREE_limit.Value("word");
			}
		}
		public int Character {
			get {
				return TREE_limit.Value("character");
			}
		}

	}
}
