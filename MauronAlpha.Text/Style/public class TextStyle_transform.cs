using System;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Style {
	
	public class TextStyle_transform : MauronCode_textComponent,
	IEquatable<TextStyle_transform>,
	I_protectable<TextStyle_transform>,
	I_instantiable<TextStyle_transform> {

		//Constructor
		public TextStyle_transform ( ) : base() { }
		public TextStyle_transform (string mode)
			: this() {
			if( !IsValidMode(mode) )
				throw Error("Invalid Mode!,(TextStyle_transform)", this, ErrorType_constructor.Instance);
			STR_mode=mode;
		}

		//Booleans
		public bool Equals (TextStyle_transform other) {
			return STR_mode==other.Mode;
		}
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public bool IsCapitalized {
			get {
				return STR_mode=="capitalized";
			}
		}
		public bool IsUppercase {
			get {
				return STR_mode=="uppercase";
			}
		}
		public bool IsLowercase {
			get {
				return STR_mode=="lowercase";
			}
		}
		public bool IsValidMode (string mode) {
			foreach( string state in ValidModes ) {
				if( state==mode )
					return true;
			}
			return false;
		}

		//Mode as String
		private string STR_mode="none";
		public string Mode {
			get {
				return STR_mode;
			}
		}

		public static string[] ValidModes=new string[] { "none", "capitalized", "uppercase", "lowercase" };

		//Methods
		public TextStyle_transform Instance {
			get {
				return new TextStyle_transform(STR_mode);
			}
		}
		public TextStyle_transform SetIsReadOnly (bool state) {
			B_isReadOnly=state;
			return this;
		}
		public TextStyle_transform SetMode (string mode) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetMode)", this, ErrorType_protected.Instance);
			if( !IsValidMode(mode) )
				throw Error("Invalid TransformStyle!,(SetMode),{"+mode+"}", this, ErrorType_scope.Instance);
			STR_mode=mode;
			return this;
		}
	} 

}
