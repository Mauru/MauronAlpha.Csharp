using MauronAlpha.MonoGame.SpaceGame.Units;

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {


	public class Tech_administration : Technology {

		public override string Name {
			get {
				return "Administration";
			}
		}

	}

	public class TurnAction : GameComponent { }

	public class Research<T> : Factory where T:I_ResearchResource where T:new() where T:ResourceType {

		public Research(): base() {

			BuildingFormula basic = new BuildingFormula();
			basic.AddCost(
				new BuildingComponent(Energy.Instance, ResourceAmount.ResearchForEnergyCost)
			);
			basic.AddCost(
				new BuildingComponent(SciencePop.Instance, ResourceAmount.ResearchForEnergyScientistAmount)
			);
			basic.AddYield(
				new BuildingComponent(new T(), ResourceAmount.ResearchForEnergyYield)
			);

		}

	}

	public class SciencePop : ResourceType {
		public static SciencePop Instance {
			get {
				return new SciencePop();
			}
		}
	}

	public class EmpireComplex : Structure {

		GameList<BuildingComponent> GeneratesPerTurn {

			get {
				return new GameList<BuildingComponent>() {
					new BuildingComponent(Food.Instance,ResourceAmount.FoodPerTurn(this)),
					new BuildingComponent(Energy.Instance,ResourceAmount.EnergyPerTurn(this)),
				};
			}

		}

	}


}
