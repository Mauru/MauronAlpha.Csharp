namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	
	public class GameValue<T> : GameComponent where T : ValueType {}
	
	public abstract class ValueType : GameComponent {
		public abstract string Name { get; }
	}
	
	public class T_Percent : ValueType {

		public override string Name {
			get { return "Percent"; }
		}

	}

	public class T_PlanetSize : ValueType {
		public override string Name { get { return "PlanetSize"; } }
	}

}
