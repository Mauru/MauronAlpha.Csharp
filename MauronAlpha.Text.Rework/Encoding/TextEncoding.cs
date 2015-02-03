using MauronAlpha.Text.Interfaces;

namespace MauronAlpha.Text.Encoding {
	
	abstract class TextEncoding:MauronCode_textComponent, 
	I_textEncoding {

		public abstract string Name { get; }

	}

}
