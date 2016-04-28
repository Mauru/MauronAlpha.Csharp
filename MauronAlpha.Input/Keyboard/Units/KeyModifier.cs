
namespace MauronAlpha.Input.Keyboard.Units {
	public abstract class KeyModifier:KeyboardComponent {
		public abstract string Name { get; }
		public bool Equals(KeyModifier other) {
			return Name.Equals(other.Name);
		}
	}
}
