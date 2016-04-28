using MauronAlpha.HandlingData;

namespace MauronAlpha.Input.Keyboard.Units {

	public abstract class SpecialKey:KeyboardComponent {

		//constructor
		public SpecialKey():base(){
		}

		//name
		public abstract string Name { get; }

		public bool Equals(SpecialKey other) {
			return Name.Equals(other.Name);
		}
	
	}

}
