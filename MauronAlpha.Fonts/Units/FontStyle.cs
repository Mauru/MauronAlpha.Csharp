using System;

using MauronAlpha.Interfaces;

namespace MauronAlpha.Fonts.Units {
	
	
	public class FontStyle:MauronAlpha_fontComponent, 
	IEquatable<FontStyle>,
	I_protectable<FontStyle>,
	I_instantiable<FontStyle> {

		//Constructors
		public FontStyle():base() {}
		public FontStyle(string name, int size) {
			STR_name = name;
			INT_size = size;
		}

		//String
		private string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}

		//Int numbers
		private int INT_size = 12;
		public int Size {
			get { return INT_size; }
		}

		//Booleans
		public bool Equals(FontStyle other) {
			return INT_size == other.Size
			&& STR_name == other.Name;
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		//Methods
		public FontStyle Instance { get {
			return new FontStyle(STR_name, INT_size);
		} }
		public FontStyle SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}
	
	}


}
