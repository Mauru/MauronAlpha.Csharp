namespace MauronAlpha.FontParser.DataObjects {
	using MauronAlpha.HandlingData;

	public class FontInfo : FontParserComponent {

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

}
