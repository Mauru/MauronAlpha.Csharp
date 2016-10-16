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
				FontHelper.FillDataMap(ref info, ref result, d);				
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
		public bool TryPositionData(char c, ref PositionData result) {
			PositionData temp = null;
			if (!DATA_characters.TryFind(c, out temp))
				return false;
			result = temp; 
			return true;
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
					return page.Index;
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
