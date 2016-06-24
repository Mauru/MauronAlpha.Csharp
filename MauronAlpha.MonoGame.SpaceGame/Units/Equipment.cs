using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Actuals;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Equippable:Item, I_RelationParty {

		public Equippable() : base(new ItemType_Equipment()) { }

		public GameParty Owner;
		public override bool IsEquipment { get { return true; } }
		public GameLocation Location;

	}

	public abstract class Item : GameObject, I_RelationTarget {

		ItemType D_type;
		public Item(ItemType type):base() { 
			D_type = type;
		}

		public bool HasMemories { get { return false; } }
		public Materials Materials;
		public Components Components;

		public override GameName Name {
			get { return D_type.Name; }
		}

		public override bool IsBeing {
			get { return D_type.IsBeing; }
		}

		public override bool IsEquipment {
			get { return D_type.IsEquipable; }
		}

		public override bool IsQuantity {
			get { return D_type.IsQuantity; }
		}

		public override bool IsResource {
			get { return D_type.IsResource; }
		}

		public override bool IsSentient {
			get { return D_type.IsSentient; }
		}
	}
	public abstract class ItemType:GameComponent {
		public abstract bool IsEquipable { get; }
		public abstract GameName Name { get; }
		public virtual bool IsSentient { get { return false; } }
		public virtual bool IsResource { get { return false; } }
		public virtual bool IsQuantity { get { return false; } }
		public virtual bool IsBeing { get { return false; } }
	}
	public class ItemType_Resource : ItemType {
		public override bool IsEquipable { get { return false; } }
		public override GameName Name { get { return new GameName("Resource"); } }
	}

	public class ItemType_Equipment : ItemType {
		public override bool IsEquipable { get { return true; } }
		public override GameName Name { get { return new GameName("Equipment"); } }
	}

	public class Materials : GameList<Material> { }

	public class Components : GameList<BuildingBlock> {
	}

	public class BuildingBlock : GameComponent { }

	public abstract class Material : Resource<T_Material> {
		public abstract GameName Name { get; }
		public abstract MaterialType MaterialType { get; }
	}
	public abstract class MaterialType : GameComponent {
		public abstract GameName Name { get; }
	}

	public class MaterialAmount : GameValue<T_Amount> { }

	public class T_Organic : MaterialType {
		public override GameName Name { get { return new GameName("Flesh"); } }
	}
	public class T_Solid : MaterialType {
		public override GameName Name { get { return new GameName("Solid"); } }
	}
	public class T_Liquid : MaterialType {
		public override GameName Name { get { return new GameName("Liquid"); } }
	}
	public class T_Gas : MaterialType {
		public override GameName Name { get { return new GameName("Gas"); } }
	}

	public class T_Amount : ValueType {
		public override GameName Name {
			get { return new GameName("Amount"); }
		}
	}

	public class T_MemoryStrength : ValueType {
		public override GameName Name {
			get { return new GameName("MemoryStrength"); }
		}
	}
	public class MemoryStrength : GameValue<T_MemoryStrength> { }

	public class Memories : GameList<Memory> { }
	public class Memory : GameComponent {

		public Memory() : this(T_Forgettable.Instance) { }
		public Memory(MemoryType type) : base() {
			MemoryType = type;
		}

		public GameTime Time;
		public MemoryType MemoryType;
		public bool Static {
			get {
				return MemoryType.IsStatic;
			}
		}

		public I_RelationParty Owner;
		public GameList<RelationTarget> Targets;

		public MemoryStrength Strength;
		public GameLocation Location;
		public MemoryScope Scope;

	}

	public abstract class MemoryType : GameComponent {

		public abstract GameName Name { get; }
		public virtual bool IsStatic { get { return false; } }

	}
	public class MemoryScope : GameComponent {

		public LocationType MinScope;
		public LocationType MaxScope;

	}

	public class T_Forgettable : MemoryType {

		public override GameName Name { get { return new GameName("Forgettable"); } }
		public static T_Forgettable Instance {
			get {
				return new T_Forgettable();
			}
		}

	}

	public class T_Irreversible : MemoryType {

		public override GameName Name { get { return new GameName("Irresversible"); } }
		public override bool IsStatic { get { return true; } }
		public static T_Irreversible Instance {
			get {
				return new T_Irreversible();
			}
		}

	}

	public interface I_GameObject {

		GameName Name { get; }

		bool IsBeing { get; }
		bool IsSentient { get; }
		bool CanBeBeing { get; }

		bool IsEquipment { get; }
		bool CanBeEquipment { get; }

		bool IsValue { get; }
		bool CanBeValue { get; }

		bool IsQuantity { get; }
		bool CanBeQuantity { get; }

		bool IsResource { get; }
		bool CanBeResource { get; }

		bool IsSkill { get; } 
		bool CanBeSkill { get; }

		bool IsStat { get; }
		bool CanBeStat { get; }

		bool CanBeRemembered { get; }
	}

	public abstract class GameObject : GameComponent, I_GameObject {

		public abstract GameName Name { get; }
		GameLocation Location;

		public abstract bool IsBeing { get; }
		public virtual bool CanBeBeing { get { return true; } }

		public abstract bool IsSentient{ get; }

		public abstract bool IsEquipment { get; }
		public virtual bool CanBeEquipment { get { return true; } }

		public virtual bool IsValue { get { return false; } }
		public virtual bool CanBeValue { get { return false; } }

		public abstract bool IsQuantity { get; }
		public virtual bool CanBeQuantity { get { return true; } }

		public abstract bool IsResource { get; }
		public virtual bool CanBeResource { get { return true; } }

		public virtual bool IsSkill { get { return false; } }
		public virtual bool CanBeSkill { get { return false; } }

		public virtual bool IsStat { get { return false; } }
		public virtual bool CanBeStat { get { return false; } }

		public virtual bool CanBeRemembered { get { return true; } }

	}

}
