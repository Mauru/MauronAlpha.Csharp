using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.MonoGame.Resources {
	public class GameFontStyle:MonoGameComponent {
		public String FontName;

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
