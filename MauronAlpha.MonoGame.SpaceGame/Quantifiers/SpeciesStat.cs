namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;

	public class SpeciesStat : GameComponent, I_BeingStat, I_SpeciesProperty {
		public GameName Name;
	}

	public class BeingStats : GameList<GameValue<T_BeingStat>> { }
	public class T_BeingStat : ValueType {
		public override GameName Name {
			get { return new GameName("BeingStat"); }
		}
	}

	public interface I_BeingStat : I_GameValue { }
	public class BeingStat<T> : GameValue<T> where T:ValueType, I_BeingStat,new() { }

	public interface I_BaseStat : I_BeingStat { }

	public class Agility : GameValue<T_Agility>, I_BaseStat { }
	public class T_Agility : ValueType {
		public override GameName Name {
			get { return new GameName("Agility"); }
		}
	}

	public class Strength : GameValue<T_Strength>, I_BaseStat { }
	public class T_Strength : ValueType {
		public override GameName Name {
			get { return new GameName("Strength"); }
		}
	}

	public class Intellect : GameValue<T_Strength>, I_BaseStat { }
	public class T_Intellect : ValueType {
		public override GameName Name {
			get { return new GameName("Intellect"); }
		}
	}

	public class DefenseStats : GameList<DefenseStat> { }
	public class DefenseStat : GameValue<T_DefenseStat>, I_BeingStat, I_DerivedGameValue { }
	public class T_DefenseStat : ValueType {
		public override GameName Name {
			get { return new GameName("DefenseStat"); }
		}
	}
	public class AttackStat : GameValue<T_AttackStat>, I_BeingStat, I_DerivedGameValue { }
	public class T_AttackStat : ValueType {
		public override GameName Name {
			get { return new GameName("AttackStat"); }
		}
	}
	public class AttackStats : GameList<AttackStat> { }

	public class Evasion : DefenseStat {
		public ValueTypes Primary { get {
			return new ValueTypes(new T_Agility());
		}}
	}
	public class Armor : DefenseStat {
		public ValueTypes Primary {
			get {
				return new ValueTypes(){ new T_Agility(), new T_Strength() };
			}
		}
	}
	public class Health : DefenseStat {
		public ValueTypes Primary {
			get {
				return new ValueTypes(new T_Strength());
			}
		}
	}
	public class Fortitude : DefenseStat {
		public ValueTypes Primary {
			get {
				return new ValueTypes(){ new T_Agility(), new T_Strength() };
			}
		}
	}
	public class Power : AttackStat {
		public ValueTypes Primary {
			get {
				return new ValueTypes() { new T_Strength(), new T_Agility() };
			}
		}
	}
	public class Will : AttackStat {
		public ValueTypes Primary {
			get {
				return new ValueTypes() { new T_Intellect() };
			}
		}
	}
	public class Morale : DefenseStat {
		public ValueTypes Primary {
			get {
				return new ValueTypes(new T_Intellect());
			}
		}
	}

	public class HitPoint : DerivedGameValue<T_HitPoints>, I_BeingStat  { }

	public class DerivedGameValue<T> : GameValue<T> where T:ValueType,new() { }
	public interface I_DerivedGameValue : I_GameValue { }

	public interface I_SpeciesProperty { }
}
