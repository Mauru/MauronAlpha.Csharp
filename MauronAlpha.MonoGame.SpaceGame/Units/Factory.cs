namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Factory:GameComponent {
		public GameList<BuildingFormula> Production;
	}

	public class BuildingFormula : GameComponent {

		GameList<BuildingComponent> Requirements;
		GameList<BuildingComponent> Output;

		public BuildingFormula() : base() {
			Requirements = new GameList<BuildingComponent>();
			Output = new GameList<BuildingComponent>();
		}
		public void AddCost(BuildingComponent cost) {
			Requirements.Add(cost);
		}
		public void AddYield(BuildingComponent yield) {
			Output.Add(yield);
		}

	}

	public class BuildingComponent:GameComponent {

		public ResourceType Resource;
		public ResourceAmount Amount;

		public BuildingComponent(ResourceType resource, ResourceAmount amount) {
			Resource = resource;
			Amount = amount;
		}

	}



}
