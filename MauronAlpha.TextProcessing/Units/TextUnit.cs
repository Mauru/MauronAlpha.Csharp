namespace MauronAlpha.TextProcessing.Units {
	
	public abstract class TextUnit:TextComponent {

		private TextUnit() : base() { }
		public TextUnit(TextUnitType unitType) : this() { }

		public abstract TextUnitType UnitType { get; }

		public bool IsSameUnitTypeAs(TextUnit other) {
			return UnitType.Equals(other.UnitType);
		}

	}

}
