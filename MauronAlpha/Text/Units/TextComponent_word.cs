using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Units {

	//A word
	public class TextComponent_word:TextComponent {
		
		//constructor
		public TextComponent_word() {}

		//Characters
		private MauronCode_dataList<TextComponent_character> DATA_characters;
		public MauronCode_dataList<TextComponent_character> Characters {
			get {
				if(DATA_characters==null){
					DATA_characters=new MauronCode_dataList<TextComponent_character>();
				}
				return DATA_characters;
			}
		}
		public TextComponent_word SetCharacters(MauronCode_dataList<TextComponent_character> chars){
			DATA_characters=chars;
			SetIsEmpty(Characters.Count<1);
			return this;
		}
		public TextComponent_word AddCharacter(TextComponent_character c){
			Characters.AddValue(c);
			SetIsEmpty(false);
			return this;
		}
		public TextComponent_character LastCharacter {
			get {
				if(Characters.Count<1){
					AddCharacter(new TextComponent_character());
				}
				return Characters.LastElement;
			}
		}
		public TextComponent_character FirstCharacter {
			get {
				if( Characters.Count<1 ) {
					AddCharacter(new TextComponent_character());
				}
				return Characters.FirstElement;
			}
		}

		//is the word "complete" (i.e. is the last character a whitespace or linebreak)
		public bool IsComplete { get {
			return ((!IsEmpty)&&(!LastCharacter.TerminatesWord));
		} }

		//is the character empty?
		private bool B_isEmpty=true;
		public bool IsEmpty {
			get { return B_isEmpty; }
		}
		public TextComponent_word SetIsEmpty (bool status) {
			B_isEmpty=status;
			return this;
		}

	}
}

