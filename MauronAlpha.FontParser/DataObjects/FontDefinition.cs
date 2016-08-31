namespace MauronAlpha.FontParser.DataObjects {
	using MauronAlpha.HandlingData;

	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.FontParser.DataObjects;

	using MauronAlpha.FileSystem.Units;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FontParser.Events;

	/// <summary> Base class of a font definition </summary>
	public class FontDefinition :FontParserComponent, I_sender<FontLoadEvent> {
		File DATA_file;

		MauronCode_dataReference<char, PositionData> DATA_characters;
		FontInfo DATA_info;
		public FontInfo FontInfo { get { return DATA_info; } }

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
			string str;
			using(System.IO.StreamReader r = DATA_file.StreamReader) {

				int ctr = 0;
				while((str = r.ReadLine()) != null) {
					ctr++;
					Line l = new Line(str);
					ParseResult rr = new ParseResult(l);
					if(rr.Key != null)
						data.Add(rr);
				}


			}
			//empty str
			str = null;

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
			S_Loaded.ReceiveEvent(new FontLoadEvent(this));
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

		public MauronCode_dataList<FontPage> FontPages {
			get {
				if(!B_isParsed)
					return new MauronCode_dataList<FontPage>();
				return DATA_info.FontPages;
			}
		}
		public PositionData PositionData(char c) {
			PositionData result = null;
			DATA_characters.TryFind(c, out result);
			return result;
		}
		public MauronCode_dataList<PositionData> PositionData(string str) {
			char[] cc = str.ToCharArray();

			MauronCode_dataList<PositionData> result = new MauronCode_dataList<PositionData>();
			foreach(char c in cc)
				result.Add(PositionData(c));

			return result;
		}

		public int IndexOfTexture(string fileName) {
			int index = -1;
			foreach(FontPage page in FontPages) {
				index++;
				if(fileName == page.FileName)
					return index;
			}
			return -1;
		}

		Subscriptions<FontLoadEvent> S_Loaded = new Subscriptions<FontLoadEvent>();
		public void Subscribe(I_subscriber<FontLoadEvent> s){
			S_Loaded.Add(s);
		}
		public void UnSubscribe(I_subscriber<FontLoadEvent> s){
			S_Loaded.Remove(s);
		}
	}

}
