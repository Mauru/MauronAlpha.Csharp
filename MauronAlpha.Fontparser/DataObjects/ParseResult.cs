namespace MauronAlpha.FontParser.DataObjects {
	using MauronAlpha.HandlingData;
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;
	
	/// <summary> Parses a BMF-Compatible line of Data into Datasets </summary>
	public class ParseResult : FontParserComponent {
		public Character Literal = new Character('"');
		public Character Escaped = new Character('\\');
		public Character KeyFinalizer = new Character('=');
		public Character SetSeperator = new Character(',');

		public Word Key;

		public MauronCode_dataStack<DataSet> Data;

		public void Finalize(ref DataSet current, ref bool isLiteral, ref bool isKey, ref MauronCode_dataStack<DataSet> result) {
			isLiteral = false;
			isKey = false;
			if (current.IsEmpty)
				return;
			result.Add(current);
			current = new DataSet();
		}

		void ParseWord(Word w, ref MauronCode_dataStack<DataSet> result, ref bool isLiteral, ref bool isKey, ref DataSet current) {
			//whitespaces always end a dataset unless we are in a literal
			if (w.IsWhiteSpace) {
				//we are in a literal so just append
				if (isLiteral) {
					current.Append(w);
				}
				else if (!current.IsEmpty) {
					Finalize(ref current, ref isLiteral, ref isKey, ref result);
				}

				return;
			}

			//this will keep all values
			Characters value = new Characters();

			//itterate over all characters
			MauronCode_dataStack<Character> characters = w.Characters.AsReversedStack;

			while (!characters.IsEmpty) {

				Character c = characters.Pop;

				//Find the key for the dataset
				if (!isKey) {

					if (c.Equals(KeyFinalizer)) {
						isKey = true;
					}
					else
						current.AppendToKey(c);

				}

				//are we in a literal
				else if (c.Equals(Literal)) {

					//end literal and dataset ( #TODO: Datasets currently do not allow literals )
					if (isLiteral) {


						if (!value.IsEmpty)
							current.Append(new Word(value.AsReversedStack));

						Finalize(ref current, ref isLiteral, ref isKey, ref result);
						return;
					}

					//we also currently can not escape literals
					isLiteral = true;

				}

				//deal with a set of data (seperated by ,)
				else if (c.Equals(SetSeperator)) {

					current.IsSetOfData = true;
					current.Append(new Word(value.AsReversedStack));

					value = new Characters();

				}

				//regular value
				else {

					value.Add(c);

				}

			}

			//we itterated over all characters
			if (!value.IsEmpty) {
				current.Append(new Word(value.AsReversedStack));
			}

			if (!isLiteral)
				Finalize(ref current, ref isLiteral, ref isKey, ref result);

			return;

		}

		//constructor
		public ParseResult(Line l)
			: base() {

			MauronCode_dataStack<DataSet> result = new MauronCode_dataStack<DataSet>();

			MauronCode_dataStack<Word> words = l.Words.AsReversedStack;

			//states
			bool isLiteral = false;
			bool hasKey = false;
			DataSet val = new DataSet();
			//itterate over all words
			while (!words.IsEmpty) {
				Word w = words.Pop;


				//make sure we get the state key
				if (Key == null) {
					Key = w;
				}
				else if (w.IsLineOrParagraphBreak) {
					Data = result;
					return;
				}
				else
					ParseWord(w, ref result, ref isLiteral, ref hasKey, ref val);
			}

			//Set final result
			Data = result;

		}

		public void DebugData() {
			string key = (Key == null) ? "NOKEY" : Key.AsString;
			string val = "";

			if (Data == null)
				val = "NODATA";
			else {

				while (!Data.IsEmpty) {
					DataSet c = Data.Pop;
					val += c.DebugData() + ";";
				}

				System.Console.WriteLine(key + ":" + val);

			}
		}
	}

}