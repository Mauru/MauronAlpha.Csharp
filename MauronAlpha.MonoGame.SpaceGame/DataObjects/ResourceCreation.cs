using MauronAlpha.MonoGame.SpaceGame;
using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Actuals;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	
	public class ExploitableSite : Site {

		public ExploitableSite(GameName name, Sector sector) : base(name,sector) { 
			ResourceCreator Creator;
			ExploitCost Cost;
			ExploitYield Yield;
			UpKeep UpKeep;
			ExploitDeposit Deposits;
			Taint Taint;
			GameList<Structure> Structures;
			Population Population;
			TacticalScore Score;
		}
	
	}
	public class ExploitYield:GameValue<T_ExploitYield> {}
	public class T_ExploitYield:ValueType {
		public override GameName Name {
			get { return new GameName("ExploitYield"); }
		}
	}

	public class UpKeep:GameValue<T_UpKeep> {}
	public class T_UpKeep:ValueType {
		public override GameName Name {
			get { return new GameName("UpKeep"); }
		}
	}

	public class TacticalScore:GameValue<T_TacticalScore> {}
	public class T_TacticalScore:ValueType {
		public override GameName Name {
			get { return new GameName("TacticalScore"); }
		}
	}

	public class ExploitCost:GameValue<T_ExploitCost> {}
	public class T_ExploitCost:ValueType {
		public override GameName Name {
			get { return new GameName("ExploitCost"); }
		}
	}

	public class ExploitDeposit:GameComponent {

		ExploitableResource Resource;

	}


	public class ResourceCreator : Structure {

		public ExploitYield PerMonth;
		public bool IsOrganic;
		public bool IsSentient;
		public bool IsMachine;
		public ExploitableSite Parent;
		public Effects Effects;
		public DefenseStats Defense;
		public Taint Taint;
		TacticalScore Score;

	}

	public class ExploitableResource : GameComponent {

		public Taint Taint;
		public ExploitableForm Form;
		public bool IsLimited;
		ExploitYield AfterRefinement;

	}

	public abstract class ExploitableForm:GameValue<T_ChemistryState> {

		public abstract GameName Name { get; }

	}
	public class T_ChemistryState:ValueType {
		public override GameName Name { get { return new GameName("ChemistryState"); } }

		public int MinValue = 0;
		public int MaxValue = 7;
		public bool CanBeNegative = false;
		public bool CanBePositive = true;

		GameList<GameName> ValueKey = new GameList<GameName>(){
			new GameName("Unknown"),new GameName("Spirit"),new GameName("Kinetic"),new GameName("Gas"), new GameName("Liquid"), new GameName("Organic"), new GameName("Solid"), new GameName("Ash")
		};


		public static GameValue<T_ChemistryState> Unknown {
			get { return new GameValue<T_ChemistryState>(false, 0); }
		}
		public static GameValue<T_ChemistryState> Spirit {
			get { return new GameValue<T_ChemistryState>(false, 1); }
		}
		public static GameValue<T_ChemistryState> Kinetic {
			get { return new GameValue<T_ChemistryState>(false, 2); }
		}
		public static GameValue<T_ChemistryState> Gas {
			get { return new GameValue<T_ChemistryState>(false, 3); }
		}
		public static GameValue<T_ChemistryState> Liquid {
			get { return new GameValue<T_ChemistryState>(false, 4); }
		}
		public static GameValue<T_ChemistryState> Organic {
			get { return new GameValue<T_ChemistryState>(false, 5); }
		}
		public static GameValue<T_ChemistryState> Solid {
			get { return new GameValue<T_ChemistryState>(false, 6); }
		}
		public static GameValue<T_ChemistryState> Ash {
			get { return new GameValue<T_ChemistryState>(false, 7); }
		}
	
	}




}
