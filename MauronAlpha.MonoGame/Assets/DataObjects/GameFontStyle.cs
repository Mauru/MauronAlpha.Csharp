namespace MauronAlpha.MonoGame.DataObjects {
	public class GameFontStyle:MonoGameComponent {
		public string FontName;

		public double FontSize = 0;
		public double LetterSpacing = 0;

		public bool IsUnderlined = false;
		public bool IsItalic = false;
		public bool IsBold = false;

		public bool IsReadOnly= true;

		public GameFontStyle(string fontName, double fontSize) : base() {
			FontName = fontName;
			FontSize = fontSize;
		}
	}
}
