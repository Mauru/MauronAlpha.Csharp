/* See DataObjects.FontDefinition for parsing process */

//BaseCode
namespace MauronAlpha.FontParser {
	using MauronAlpha.ExplainingCode;

	public class FontParserComponent :MauronCode_component { }
}

//Events
namespace MauronAlpha.FontParser.Events {
	using MauronAlpha.Events.Units;
	using MauronAlpha.FontParser.DataObjects;

	public class FontLoadEvent :EventUnit_event {

		FontDefinition _font;
		public FontDefinition Font {
			get {
				return _font;
			}
		}

		public FontLoadEvent(FontDefinition font) : base("Loaded") {
			_font = font;
		}
	}
}

//Common DataObjects
namespace MauronAlpha.FontParser.DataObjects {
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;
	using MauronAlpha.HandlingData;

	/// <summary> A parsed set of data, auto-empties on read </summary>
	public class DataSet :FontParserComponent {
		MauronCode_dataStack<Word> Data;

		public Word Key;
		public bool HasKey {
			get { return Key != null; }
		}
		public bool HasValue {
			get {
				return !Data.IsEmpty;
			}
		}
		public bool IsEmpty {
			get { return Key == null; }
		}

		public bool IsSetOfData = false;

		public DataSet() : base() { }

		public long Count {
			get {
			if(Data == null)
				return 0;
			return Data.Count;
			}
		}

		/// <summary> Append word to value </summary>
		public void Append(Word w) {
			if(Data == null) { 
				Data = new MauronCode_dataStack<Word>();
			}
			Data.Add(w);

		}
		/// <summary> Append a character to the key </summary>
		public void AppendToKey(Character c) {
			if(Key == null)
				Key = new Word();
			Key.Add(c);
		}

		/// <summary> Return value without emptying dataset </summary>
		public string PreviewValue() {
			string result = "";
			MauronCode_dataStack<Word> p = new MauronCode_dataStack<Word>();
			if(Data==null) {
				return "EMPTY";
			}
			while(!Data.IsEmpty) {
				Word w = Data.Pop;
				result += w.AsVisualString;
				p.Add(w);
			}
			Data = p;
			return result;
		}

		/// <summary> Read value as string, empties object </summary>
		public string ValueAsString {
			get {
				if(Data == null)
					return "";
				string result = "";
				while(!Data.IsEmpty) {
					Word w = Data.Pop;
					result = w.AsString+result;
				}
				return result;
			}
		}
		public int ValueAsInt {
			get {
				if( IsEmpty || IsSetOfData )
					return 0;

				Word w = Data.Pop;
				return WordAsInt32(w);
			}
		}

		public int WordAsInt32(Word w) {
			return System.Int32.Parse(w.AsString);
		}
		public bool ValueAsBool {
			get {
				if( IsEmpty || IsSetOfData )
					return false;
				
				Word w = Data.Pop;
				return WordAsInt32(w) > 0;
			}
		}
		public Distance ValueAsDistance {
			get {
				if(!IsSetOfData)
					return new Distance();
				Distance result = new Distance();
				int ctr = 0;
				while(!Data.IsEmpty) {
					Word w = Data.Pop;
					if(ctr == 0)
						result.Top = WordAsInt32(w);
					else if(ctr == 1)
						result.Right = WordAsInt32(w);
					else if(ctr == 2)
						result.Bottom = WordAsInt32(w);
					else if(ctr == 3)
						result.Left = WordAsInt32(w);

					ctr++;
				}
				return result;
			}
		}
		public Distance ValueAsVector {
			get {
				if(!IsSetOfData)
					return new Distance();
				int ctr = 0;
				Distance result = new Distance();
				while(!Data.IsEmpty) {
					Word w = Data.Pop;
					int n = 0;
					if(ctr == 0) {
						n = WordAsInt32(w);
						result.Left = n;
						result.Right = n;
					} else if(ctr == 1) { 
						n = WordAsInt32(w);
						result.Bottom = n;
						result.Bottom = n;
					}
					ctr++;
				}
				return result;
			}
		}

		public string DebugData() {
			string key = (Key == null) ? "NOKEY" : Key.AsString;
			string val = "";

			if(Data == null) {

				val = "NODATA";

			} else {

				val = "VALUES(" + Data.Count + ")";

			}

			return "{" + key + ":" + val + "}";
		}
	}

	/// <summary> Parses a BMF-Compatible line of Data into Datasets </summary>
	public class ParseResult :FontParserComponent {
		public Character Literal = new Character('"');
		public Character Escaped = new Character('\\');
		public Character KeyFinalizer = new Character('=');
		public Character SetSeperator = new Character(',');

		public Word Key;

		public MauronCode_dataStack<DataSet> Data;

		public void Finalize(ref DataSet current, ref bool isLiteral, ref bool isKey, ref MauronCode_dataStack<DataSet> result) {
			isLiteral = false;
			isKey = false;
			if(current.IsEmpty)
				return;
			result.Add(current);
			current = new DataSet();
		}

		void ParseWord(Word w, ref MauronCode_dataStack<DataSet> result, ref bool isLiteral, ref bool isKey, ref DataSet current) {
			//whitespaces always end a dataset unless we are in a literal
			if(w.IsWhiteSpace) {
				//we are in a literal so just append
				if(isLiteral) {
					current.Append(w);
				} else if(!current.IsEmpty) {
					Finalize(ref current, ref isLiteral, ref isKey, ref result);
				}

				return;
			}

			//this will keep all values
			Characters value = new Characters();

			//itterate over all characters
			MauronCode_dataStack<Character> characters = w.Characters.AsReversedStack;

			while(!characters.IsEmpty) {

				Character c = characters.Pop;
				
				//Find the key for the dataset
				if(!isKey) {
					
					if(c.Equals(KeyFinalizer)) {
						isKey = true;
					} else
						current.AppendToKey(c);

				}
				
				//are we in a literal
				else if(c.Equals(Literal)) {

					//end literal and dataset ( #TODO: Datasets currently do not allow literals )
					if(isLiteral) { 


						if(!value.IsEmpty)
							current.Append(new Word(value.AsReversedStack));

						Finalize(ref current, ref isLiteral, ref isKey, ref result);
						return;
					}

					//we also currently can not escape literals
					isLiteral = true;

				}

				//deal with a set of data (seperated by ,)
				else if(c.Equals(SetSeperator)) {

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
			if(!value.IsEmpty) { 
				current.Append(new Word(value.AsReversedStack));
			}

			if(!isLiteral)
				Finalize(ref current, ref isLiteral, ref isKey, ref result);

			return;
			
		}

		//constructor
		public ParseResult(Line l) : base() {

			MauronCode_dataStack<DataSet> result = new MauronCode_dataStack<DataSet>();

			MauronCode_dataStack<Word> words = l.Words.AsReversedStack;

			//states
			bool isLiteral = false;
			bool hasKey = false;
						DataSet val = new DataSet();
			//itterate over all words
			while(!words.IsEmpty) {
				Word w = words.Pop;

				
				//make sure we get the state key
				if(Key == null) {
					Key = w;
				} else if(w.IsLineOrParagraphBreak) {
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

			if(Data == null)
				val = "NODATA";
			else {

			while(!Data.IsEmpty) {
				DataSet c = Data.Pop;
				val += c.DebugData()+";";
			}

			System.Console.WriteLine(key + ":" + val);

			}
		}
	}

	public class PositionData :FontParserComponent {

		public int UnicodeId;

		public int X = 0;
		public int Y = 0;

		public int Width = 0;
		public int Height = 0;

		/// <summary> How much the current position should be offset when copying the image from the texture to the screen. </summary>
		public int XOffset = 0;
		public int YOffset = 0;

		/// <summary> Unclear. How much the current position should be advanced after drawing the character. </summary>
		public int XAdvance = 0;

		public int FontPage = 0;

		/// <summary> The texture channel where the character image is found (1 = blue, 2 = green, 4 = red, 8 = alpha, 15 = all channels). </summary>
		public int Channel = 0;

	}

	public class FontPage :FontParserComponent {

		public FontPage() : base() { }

		public int Index = 0;
		public string FileName;

	}
	public class FontInfo :FontParserComponent {

		public string Name = "FontInfo.NULL";
		public int Size = 0;
		public int LineHeight = 0;
		public int BaseHeight = 0;

		public Distance Spacing = new Distance();
		public Distance Padding = new Distance();

		public bool IsBold = false;
		public bool IsItalic = false;
		public bool IsUnicode = false;
		public bool IsSmoothed = false;
		public bool IsAntialiased { get { return SuperSamplingLevel > 1; } }
		public bool IsOutline { get { return OutlineThickness > 0; } }
		public bool CharsArePackedIntoChannels = false;

		public string CharSet;
		public int HeightStretchPercent = 100;

		public int SuperSamplingLevel = 1;
		public int OutlineThickness = 0;

		public int TextureWidth = 0;
		public int TextureHeight = 0;

		public int PageCount = 1;
		public int CharCount = 0;

		/// <summary> How the channels store data
		///  0 : glyph data, 
		///  1 : outline,
		///  2 : glyph, outline
		///  3 : 0
		///  4 : 1
		/// </summary>
		public int AlphaState = 0;
		public int RedState = 0;
		public int GreenState = 0;
		public int BlueState = 0;

		/// <summary> The Number of Textures </summary>
		public MauronCode_dataList<FontPage> FontPages = new MauronCode_dataList<FontPage>();
		
	}

	//Represents a distance in carthesian space
	public class Distance :FontParserComponent {

		public int Top = 0;
		public int Left = 0;
		public int Bottom = 0;
		public int Right = 0;

	}

}