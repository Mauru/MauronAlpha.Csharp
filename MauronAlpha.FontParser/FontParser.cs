namespace MauronAlpha.FontParser {
	//using System.IO;

	using MauronAlpha.ExplainingCode;
	using MauronAlpha.HandlingData;

	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.FontParser.DataObjects;

	using MauronAlpha.FileSystem.Units;

	public class FontParserComponent :MauronCode_component { }

	/// <summary> Base class of a font definition </summary>
	public class FontDefinition :FontParserComponent {

		File DATA_file;

		MauronCode_dataReference<char, PositionData> DATA_characters;
		FontInfo DATA_info;

		//constructor
		public FontDefinition(File f): base() {
			DATA_file = f;

		}

		public string FontName {
			get {
				return DATA_info.Name;
			}
		}

		bool B_isBusy = false;
		public bool IsBusy { get { return B_isBusy; } }

		bool B_isParsed = false;
		public bool IsParsed { get { return B_isParsed; } }

		FontInfo ParseFontInfo(FontInfo f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			while(!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;

				if(key == "face")
					f.Name = d.ValueAsString;
				else if(key == "size")
					f.Size = System.Math.Abs(d.ValueAsInt);
				else if(key == "bold")
					f.IsBold = d.ValueAsBool;
				else if(key == "italic")
					f.IsItalic = d.ValueAsBool;
				else if(key == "charset")
					f.CharSet = d.ValueAsString;
				else if(key == "unicode")
					f.IsUnicode = d.ValueAsBool;
				else if(key == "stretchH")
					f.HeightStretchPercent = d.ValueAsInt;
				else if(key == "smooth")
					f.IsSmoothed = d.ValueAsBool;
				else if(key == "aa")
					f.SuperSamplingLevel = d.ValueAsInt;
				else if(key == "padding")
					f.Padding = d.ValueAsDistance;
				else if(key == "spacing")
					f.Spacing = d.ValueAsVector;
				else if(key == "outline")
					f.OutlineThickness = d.ValueAsInt;
				else
					missed.Add(key);
			}

			return f;		
		}
		FontInfo ParseCommonInfo(FontInfo f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			while(!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if(key == "lineHeight")
					f.LineHeight = d.ValueAsInt;
				else if(key == "base")
					f.BaseHeight = d.ValueAsInt;
				else if(key == "scaleW")
					f.TextureWidth = d.ValueAsInt;
				else if(key == "scaleH")
					f.TextureHeight = d.ValueAsInt;
				else if(key == "pages")
					f.PageCount = d.ValueAsInt;
				else if(key == "packed")
					f.CharsArePackedIntoChannels = d.ValueAsBool;
				else if(key == "alphaChnl")
					f.AlphaState = d.ValueAsInt;
				else if(key == "redChnl")
					f.RedState = d.ValueAsInt;
				else if(key == "greenChnl")
					f.GreenState = d.ValueAsInt;
				else if(key == "blueChnl")
					f.BlueState = d.ValueAsInt;
				else
					missed.Add(key);
			}
			return f;
		}
		FontInfo ParsePageData(FontInfo f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			FontPage p = new FontPage();
			while(!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if(key == "id")
					p.Index = d.ValueAsInt;
				else if(key == "file")
					p.FileName = d.ValueAsString;
				else
					missed.Add(key);
			}
			f.FontPages.Add(p);
			return f;
		}
		FontInfo ParseCharCount(FontInfo f, MauronCode_dataStack<DataSet> data) { 
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			while(!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if(key == "id")
					f.CharCount = d.ValueAsInt;
				else
					missed.Add(key);
			}
			return f;
		}
		MauronCode_dataReference<char, PositionData> ParseCharPositionData(MauronCode_dataReference<char, PositionData> f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			PositionData p = new PositionData();
			while(!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if(key == "id")
					p.UnicodeId = d.ValueAsInt;
				else if(key == "x")
					p.X = d.ValueAsInt;
				else if(key == "y")
					p.Y = d.ValueAsInt;
				else if(key == "width")
					p.Width = d.ValueAsInt;
				else if(key == "height")
					p.Height = d.ValueAsInt;
				else if(key == "xoffset")
					p.XOffset = d.ValueAsInt;
				else if(key == "yoffset")
					p.YOffset = d.ValueAsInt;
				else if(key == "xadvance")
					p.XAdvance = d.ValueAsInt;
				else if(key == "page")
					p.FontPage = d.ValueAsInt;
				else if(key == "chnl")
					p.Channel = d.ValueAsInt;
			}

			char c = (char) p.UnicodeId;

			f.SetValue(c, p);

			return f;
		}
		void FillDataMap(ref FontInfo info, ref MauronCode_dataReference<char,PositionData> f, ParseResult d) {

			string key = d.Key.AsString;
			if(key == "info") {
				info = ParseFontInfo(info, d.Data);
				return;
			} else if(key == "common") { 
				info = ParseCommonInfo(info, d.Data);
				return;
			} else if(key == "page") {
				info = ParsePageData(info, d.Data);
				return;
			} else if(key == "chars") {
				info = ParseCharCount(info, d.Data);
				return;
			} else if(key == "char") {
				f = ParseCharPositionData(f, d.Data);
				return;
			}
			return;
		}

		public void Parse() {
			if(B_isBusy)
				return;
			B_isBusy = true;

			MauronCode_dataStack<ParseResult> data = new MauronCode_dataStack<ParseResult>();
			using(System.IO.StreamReader r = new System.IO.StreamReader(DATA_file.Path)) {
				string str;
				int ctr = 0;
				while((str = r.ReadLine()) != null) {
					ctr++;
					Line l = new Line(str);
					ParseResult rr = new ParseResult(l);
					if(rr.Key != null)
						data.Add(rr);
				}
			}

			FontInfo info = new FontInfo();
			MauronCode_dataReference<char,PositionData> result = new MauronCode_dataReference<char, PositionData>();

			//We now got the dataset - time to convert into fontInfo
			while(!data.IsEmpty) {
				ParseResult d = data.Pop;
				//d.DebugData();
				FillDataMap(ref info, ref result, d);				
			}

			//store the data
			DATA_characters = result;
			DATA_info = info;

			B_isBusy = false;
			B_isParsed = true;
		}

		MauronCode_dataList<File> DATA_files;
		public MauronCode_dataList<File> Files {
			get {
				if(!B_isParsed)
					return new MauronCode_dataList<File>();
				if(DATA_files == null) {
					DATA_files = new MauronCode_dataList<File>();
					foreach(FontPage p in DATA_info.FontPages) {
						File n = new File(DATA_file.Directory, p.FileName);
						DATA_files.Add(n);
					}
				}
				return DATA_files;
			}
		}

		public MauronCode_dataList<PositionData> PositionData(string str) {
			char[] cc = str.ToCharArray();

			MauronCode_dataList<PositionData> result = new MauronCode_dataList<PositionData>();
			foreach(char c in cc) {
				PositionData r = null;
				DATA_characters.TryFind(c, out r);
				result.Add(r);
			}
			return result;
		}
	}

	public class FontParser :FontParserComponent { }
}

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