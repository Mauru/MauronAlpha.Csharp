namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Technology:GameComponent {

		GameList<BuildingComponent> Cost;
		GameList<ModuleAbility> Modules;
		GameList<Structure> Structures;
		GameList<MapAction> MapActions;
		GameList<BattleAction> BattleAction;

	}

	public interface I_ResearchResource { }

	public class TechPoint_Physics:ResourceType,I_ResearchResource {}
	public class TechPoint_Engineering : ResourceType,I_ResearchResource { }
	public class TechPoint_Biology : ResourceType, I_ResearchResource { }

	public class TechBranch : GameComponent { }
}
