namespace MauronAlpha.FontParser {

	using MauronAlpha.HandlingData;
	using MauronAlpha.FontParser.DataObjects;

	public class FontHelper : FontParserComponent {

		public static FontInfo ParseFontInfo(FontInfo f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			while (!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;

				if (key == "face")
					f.Name = d.ValueAsString;
				else if (key == "size")
					f.Size = System.Math.Abs(d.ValueAsInt);
				else if (key == "bold")
					f.IsBold = d.ValueAsBool;
				else if (key == "italic")
					f.IsItalic = d.ValueAsBool;
				else if (key == "charset")
					f.CharSet = d.ValueAsString;
				else if (key == "unicode")
					f.IsUnicode = d.ValueAsBool;
				else if (key == "stretchH")
					f.HeightStretchPercent = d.ValueAsInt;
				else if (key == "smooth")
					f.IsSmoothed = d.ValueAsBool;
				else if (key == "aa")
					f.SuperSamplingLevel = d.ValueAsInt;
				else if (key == "padding")
					f.Padding = d.ValueAsDistance;
				else if (key == "spacing")
					f.Spacing = d.ValueAsVector;
				else if (key == "outline")
					f.OutlineThickness = d.ValueAsInt;
				else
					missed.Add(key);
			}

			return f;
		}
		public static FontInfo ParseCommonInfo(FontInfo f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			while (!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if (key == "lineHeight")
					f.LineHeight = d.ValueAsInt;
				else if (key == "base")
					f.BaseHeight = d.ValueAsInt;
				else if (key == "scaleW")
					f.TextureWidth = d.ValueAsInt;
				else if (key == "scaleH")
					f.TextureHeight = d.ValueAsInt;
				else if (key == "pages")
					f.PageCount = d.ValueAsInt;
				else if (key == "packed")
					f.CharsArePackedIntoChannels = d.ValueAsBool;
				else if (key == "alphaChnl")
					f.AlphaState = d.ValueAsInt;
				else if (key == "redChnl")
					f.RedState = d.ValueAsInt;
				else if (key == "greenChnl")
					f.GreenState = d.ValueAsInt;
				else if (key == "blueChnl")
					f.BlueState = d.ValueAsInt;
				else
					missed.Add(key);
			}
			return f;
		}
		public static FontInfo ParsePageData(FontInfo f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			FontPage p = new FontPage();
			while (!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if (key == "id")
					p.Index = d.ValueAsInt;
				else if (key == "file")
					p.FileName = d.ValueAsString;
				else
					missed.Add(key);
			}
			f.FontPages.Add(p);
			return f;
		}
		public static FontInfo ParseCharCount(FontInfo f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			while (!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if (key == "id")
					f.CharCount = d.ValueAsInt;
				else
					missed.Add(key);
			}
			return f;
		}
		public static MauronCode_dataReference<char, PositionData> ParseCharPositionData(MauronCode_dataReference<char, PositionData> f, MauronCode_dataStack<DataSet> data) {
			MauronCode_dataStack<string> missed = new MauronCode_dataStack<string>();
			PositionData p = new PositionData();
			while (!data.IsEmpty) {
				DataSet d = data.Pop;
				string key = d.Key.AsString;
				if (key == "id")
					p.UnicodeId = d.ValueAsInt;
				else if (key == "x")
					p.X = d.ValueAsInt;
				else if (key == "y")
					p.Y = d.ValueAsInt;
				else if (key == "width")
					p.Width = d.ValueAsInt;
				else if (key == "height")
					p.Height = d.ValueAsInt;
				else if (key == "xoffset")
					p.XOffset = d.ValueAsInt;
				else if (key == "yoffset")
					p.YOffset = d.ValueAsInt;
				else if (key == "xadvance")
					p.XAdvance = d.ValueAsInt;
				else if (key == "page")
					p.FontPage = d.ValueAsInt;
				else if (key == "chnl")
					p.Channel = d.ValueAsInt;
			}

			char c = (char)p.UnicodeId;

			f.SetValue(c, p);

			return f;
		}
		public static void FillDataMap(ref FontInfo info, ref MauronCode_dataReference<char, PositionData> f, ParseResult d) {

			string key = d.Key.AsString;
			if (key == "info") {
				info = ParseFontInfo(info, d.Data);
				return;
			}
			else if (key == "common") {
				info = ParseCommonInfo(info, d.Data);
				return;
			}
			else if (key == "page") {
				info = ParsePageData(info, d.Data);
				return;
			}
			else if (key == "chars") {
				info = ParseCharCount(info, d.Data);
				return;
			}
			else if (key == "char") {
				f = ParseCharPositionData(f, d.Data);
				return;
			}
			return;
		}

	}

}