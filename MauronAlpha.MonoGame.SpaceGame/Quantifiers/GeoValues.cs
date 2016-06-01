
namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	public class GeoValues:GameList<GeoValue> {
	}

	public class GeoValue : GameValue<T_GeoValue> { }
	public class T_GeoValue : ValueType {
		public override GameName Name {
			get { return new GameName("GeoValue"); }
		}
	}

	public class T_PlanetSize : ValueType {
		public override GameName Name {
			get { return new GameName("PlanetSize"); }
		}
	}
	public class T_DistanceFromStarCentre : ValueType {

		public override GameName Name {
			get { return new GameName("DistanceFromStarCentre"); }
		}
	}

}
