namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	public class CombatValues:GameList<GameValue<T_CombatValue>> {
	}

	public class T_CombatValue : ValueType {
		public override GameName Name {
			get { return new GameName("CombatValue"); }
		}
	}

	public class ShieldPoints : GameValue<T_Shields> { }
	public class ArmorPoints : GameValue<T_Armor> { }

	public class HealthPoints : GameValue<T_HitPoints> { }

	public class T_Shields : ValueType {
		public override GameName Name {
			get { return new GameName("Shields"); }
		}
	}
	public class T_Armor : ValueType {
		public override GameName Name {
			get { return new GameName("Armor"); }
		}
	}
	public class T_Morale : ValueType {
		public override GameName Name {
			get { return new GameName("Morale"); }
		}
	}
	public class T_HitPoints : ValueType {
		public override GameName Name {
			get { return new GameName("HitPoints"); }
		}
	}
}
