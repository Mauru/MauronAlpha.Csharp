using System;

using MauronAlpha.Colors.Units;
using MauronAlpha.Fonts;
using MauronAlpha.Fonts.Units;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;


namespace MauronAlpha.Text.Style {

	public class TextStyle : MauronCode_textComponent,
	I_protectable<TextStyle>,
	I_instantiable<TextStyle>,
	IEquatable<TextStyle> {

		//Constructors
		public TextStyle():base() {}
		public TextStyle(bool isBold, bool isItalic, bool isUnderlined, bool isStrikeThrough, TextStyle_transform transformStyle, FontStyle fontStyle, RGBColor color, RGBColor background):this() {
			B_isBold = isBold;
			B_isItalic = isItalic;
			B_isUnderlined = isUnderlined;
			B_isStrikeThrough = isStrikeThrough;
			STYLE_transform = transformStyle;
			STYLE_font = fontStyle;
			COLOR_text = color;
			COLOR_background = background;
		}

		//Booleans
		public bool Equals(TextStyle other) {
			return B_isBold == other.IsBold
			&& B_isItalic == other.IsItalic
			&& B_isUnderlined == other.IsUnderlined
			&& B_isStrikeThrough == other.IsStrikeThrough
			&& STYLE_transform == other.Transform
			&& COLOR_text == other.Color
			&& COLOR_background == other.BackgroundColor
			&& STYLE_font == other.FontStyle;
		}
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		private bool B_isBold = false;
		public bool IsBold {
			get {
				return B_isBold;
			}
		}
		private bool B_isItalic = false;
		public bool IsItalic { get {
			return B_isItalic;
		}}
		private bool B_isUnderlined = false;
		public bool IsUnderlined {
			get { return B_isUnderlined; }
		}
		private bool B_isStrikeThrough = false;
		public bool IsStrikeThrough {
			get {
				return B_isStrikeThrough;
			}
		}

		private TextStyle_transform STYLE_transform = new TextStyle_transform();
		public TextStyle_transform Transform {
			get {
				return STYLE_transform;
			}
		}

		private RGBColor COLOR_text = new RGBColor();
		public RGBColor Color {
			get {
				return COLOR_text;
			}
		}
		private RGBColor COLOR_background = new RGBColor();
		public RGBColor BackgroundColor {
			get {
				return COLOR_background;
			}
		}
		
		private FontStyle STYLE_font = new FontStyle_default();
		public FontStyle FontStyle {
			get {
				return STYLE_font;
			}
		}

		//Methods
		public TextStyle Instance {
			get {
				return new TextStyle(B_isBold, B_isItalic, B_isUnderlined, B_isStrikeThrough, STYLE_transform, STYLE_font, COLOR_text, COLOR_background);
			}
		}
		public TextStyle SetIsReadOnly (bool state) {
			B_isReadOnly = state;
			return this;
		}

	}

}
