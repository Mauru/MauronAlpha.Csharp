using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {
	
	public class Sun : Orbital<OT_Sun> {

		public Sun(MapLocation location) : base(location) { }

	}

	public class OT_Sun : OrbitalType { }

}
