using MauronAlpha.MonoGame.SpaceGame.Units;

using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
using MauronAlpha.MonoGame.SpaceGame.Actuals;

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {


	public class Tech_administration : Technology {

		public GameName Name {
			get {
				return new GameName("Administration");
			}
		}

	}

	public class TurnAction : GameComponent { }

	public class Research<T> : Factory where T:I_ResourceDefinition,new() {

		public Research(): base() {

			BuildingFormula basic = new BuildingFormula();
			basic.AddCost(
				new BuildingComponent(Energy.Instance, ResourceAmount.ResearchForEnergyCost)
			);
			basic.AddCost(
				new BuildingComponent(SciencePop.Instance, ResourceAmount.ResearchForEnergyScientistAmount)
			);
			TechPoint<T> yield = new TechPoint<T>();
			basic.AddYield(
				new BuildingComponent(yield, ResourceAmount.ResearchForEnergyYield)
			);

		}

	}

	public class SciencePop : ResourceType, I_ResourceDefinition {
		public static SciencePop Instance {
			get {
				return new SciencePop();
			}
		}

		public I_ResourceDefinition InterfaceInstance() {
			return SciencePop.Instance;
		}
	}

	public class TechPoint<T> : ResourceType, I_ResourceDefinition where T : I_ResourceDefinition, new() {

		public TechPoint() : base() { }

		public I_ResourceDefinition InterfaceInstance() {
			return new TechPoint<T>();
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
