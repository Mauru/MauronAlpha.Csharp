using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TextProcessing.Units {
	
	public abstract class TextUnitType:TextComponent {

		public abstract string Name { get; }

		public bool Equals(TextUnitType other) {
			return Name.Equals(other.Name);
		}

		public bool Equals(TextUnit other) {
			return other.UnitType.Equals(this);
		}

		public bool IsNull {
			get {
				return Equals(TextUnitTypes.Null);
			}
		}
	}

	public class TextUnitType_none : TextUnitType {

		public override string Name {
			get { return "None"; }
		}

		public static TextUnitType_none Instance {
			get {
				return new TextUnitType_none();
			}
		}
	}

}
