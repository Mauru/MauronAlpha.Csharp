using System;
using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Context {

/*=========================================================================
A Result of a TextContext.Solve directive
=========================================================================*/
	public class TextContextSolver : MauronCode_textComponent {
		
		//DataTrees
		private MauronCode_dataTree<string,int> Index = new MauronCode_dataTree<string,int>(SharedDataKeys);
		private MauronCode_dataTree<string,int> Count = new MauronCode_dataTree<string,int>(SharedDataKeys);

		//DataKeys
		public static string[] SharedDataKeys = new string[] { "line", "word", "character" };
		public static string[] SharedDataComponents = new string[] { "Index", "Count" };

		//constructor
		public TextContextSolver(TextContext context, TextUnit_text text){
			
			//We Validate
			
				//start off by collecting the counts of the components in the text
				Count.SetValue("line", text.LineCount);
				Count.SetValue("word", text.WordCount);
				Count.SetValue("character", text.CharacterCount);

				//

			//We set the Result

		}


		private TextContext TXT_source;
		private MauronCode_textComponent TXT_textFragment;

		private MauronCode_dataList<TextContextException> DATA_Exceptions;
		public MauronCode_dataList<TextContextException> Exceptions {
			get {
				if(DATA_Exceptions==null){
					Exception("DATA_Exceptions can not be Null",this, ErrorResolution.Create);
					DATA_Exceptions = new MauronCode_dataList<TextContextException>();
				}
				return DATA_Exceptions;
			}
		}
	
	}
}
