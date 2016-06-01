using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Technology:GameComponent {

		GameList<BuildingComponent> Cost;
		GameList<ModuleAbility> Modules;
		GameList<Structure> Structures;
		GameList<MapAction> MapActions;
		GameList<BattleAction> BattleAction;

	}

	public interface I_ResearchResource { }

	public class TechPoint_Physics:ResourceType,I_ResearchResource {
		public override GameName Name {
			get { return new GameName("TechPoint_Physics"); }
		}
	}
	public class TechPoint_Engineering : ResourceType,I_ResearchResource {
		public override GameName Name {
			get { return new GameName("TechPoint_Engineering"); }
		}
	}
	public class TechPoint_Biology : ResourceType, I_ResearchResource {
		public override GameName Name {
			get { return new GameName("TechPoint_Biology"); }
		}
	}

	public class TechBranch : GameComponent { }
}
