using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Galaxy:GameComponent {

		Universe Universe;

		public Galaxy(Universe universe) : base() {
			Universe = universe;
		}

		public GameValue<T_StarSystemLimit> SystemLimit;

		public GameList<StarSystem> StarSystems;
		public GameList<StarSystem> InLimbo = new GameList<StarSystem>();
		public GameList<StarSystem> Buffered = new GameList<StarSystem>();

		public bool NoNewSystemsAllowed {
			get {
				if (SystemLimit == null)
					return false;
				if (StarSystems != null) {
					if(SystemLimit.IsSmallerThan(StarSystems.Count))
						return false;
					if(SystemLimit.IsSmallerThan(StarSystems.Count))
						return false;

				}
				return true;
			}
		}

		public StarSystem NewStarSystem( I_RuleSet<RuleSet_MapGenerator> rules) {
		
			if(!NoNewSystemsAllowed)
				return BufferedBecauseNotAllowed(this,rules);

			StarSystem newStar = new StarSystem(this, true);
			InLimbo.Add(newStar);
			return newStar;		
		}
		public StarSystem BufferedBecauseNotAllowed(Galaxy galaxy, I_RuleSet<RuleSet_MapGenerator> rules) {
			StarSystem n = new StarSystem(this, true);
			return n;
		}
	}

	public  class T_StarSystemLimit:ValueType {
		public override GameName Name {
			get { return new GameName("StarSystemLimit"); }
		}
	}

}
