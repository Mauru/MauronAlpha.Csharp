namespace MauronAlpha.FontParser.DataObjects {
	public class PositionData : FontParserComponent {

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
}