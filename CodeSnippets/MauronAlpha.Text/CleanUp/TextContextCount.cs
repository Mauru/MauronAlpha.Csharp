using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Context {
	//An object tracking how many textcomponents are in a component
	public class TextContextCount : TextComponent_context {

		//DataTrees
		private MauronCode_dataTree<string, int> TREE_count=new MauronCode_dataTree<string, int>(SharedDataKeys, SharedDataDefaults);
		//DataKeys
		private static string[] SharedDataKeys=new string[] { "paragraph", "line", "word", "character" };
		//Default Values
		private static int[] SharedDataDefaults=new int[] { 0, 0, 0, 0 };

		//constructor
		private TextContextCount ( ) { }
		public TextContextCount (int paragraph, int line, int word, int character) {
			TREE_count.SetValues(new int[]{
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
				return TREE_count.Value("paragraph");
			}
		}
		public int Line {
			get {
				return TREE_count.Value("line");
			}
		}
		public int Word {
			get {
				return TREE_count.Value("word");
			}
		}
		public int Character {
			get {
				return TREE_count.Value("character");
			}
		}

	}
}
