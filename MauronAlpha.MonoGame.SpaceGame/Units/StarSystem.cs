using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Actuals;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class StarSystem:GameComponent {

		private GameList<Planet> Planets = new GameList<Planet>();
		public GameList<Orbital<OT_habitable>> Habitables { 
			get {
				GameList<Orbital<OT_habitable>> result = new GameList<Orbital<OT_habitable>>();
				foreach (Planet p in Planets) {
					result.Add(p);
				}
				return result;
			}}

		public Galaxy Galaxy;

		private bool B_isBuffered = true;
		public bool IsBuffered {
			get {
				return B_isBuffered;
			}
		}

		public StarSystem(Galaxy galaxy): this(galaxy,true) {
			Galaxy = galaxy;
		}
		public StarSystem(Galaxy galaxy, bool IsBuffered) : base() {
			B_isBuffered = IsBuffered;
			Galaxy = galaxy;
		}

		public void AddHabitable(Planet p) {
				
		}

	}
}
