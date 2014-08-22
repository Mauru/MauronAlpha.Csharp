using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : TextComponent {

		//constructor
		public TextComponent_line (TextComponent_text parent, TextContext context) {}

		private MauronCode_dataList<TextComponent_word> Words = new MauronCode_dataList<TextComponent_word>();

		private TextComponent_text TXT_parent;
		public TextComponent_text Parent {
			get {
				if(TXT_parent==null) {
					NullError("Parent can't be null", this, typeof(TextComponent_text));
				}
				return TXT_parent;
			}
		}
		private TextComponent_line SetParent(TextComponent_text parent){
			TXT_parent=parent;
			return this;	
		}

		private TextContext TXT_context;
		public TextContext Context {
			get {
				if(TXT_context==null){
					NullError("Context can't be null", this, typeof(TextContext));
				}
				return TXT_context;
			}
		}
		private TextComponent_line SetContext(TextContext context){
			TXT_context=context;
			return this;
		}


		public TextComponent_line AddWord(TextComponent_word word){

			if(Words.Count>0&&Words.LastElement.EndsLine){

				//Add to new line
				TextContext context = Context.Instance;
				context.SetLineOffset (context.LineOffset + 1)
				.SetWordOffset (0)
				.SetCharacterOffset(0);

				Parent.AddLineAtContext (context);
				return Parent.LineByContext(context).AddWord (word);

			}
			Words.AddValue (word);
			return this;
		}

		public TextComponent_word LastWord {
			get { 
				if (Words.Count < 1) {
					Error ("Invalid Index {LastWord}", this);
				}
				return Words.LastElement;
			}
		}

		public int WordCount {
			get { 
				return Words.Count;
			}
		}


	}
}