namespace MauronAlpha.MonoGame.Logic.Locations.Objects {
	using MauronAlpha.MonoGame;

	using MauronAlpha.MonoGame.Logic.Actions.Collections;
	using MauronAlpha.MonoGame.Logic.Entities.DataObjects;

    public abstract class SiteObject:MonoGameComponent {

		public abstract MovementTypes AllowedMovement { get; }
		public abstract ActionTypes AllowedActions { get; }
		public abstract Health Health { get; }
		
	}
}

namespace MauronAlpha.MonoGame.Logic.Entities.DataObjects {

	public class Health :MonoGameComponent { }

}

namespace MauronAlpha.MonoGame.Logic.Actions.Collections {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.MonoGame.Logic.Actions.DataObjects;

	public class MovementTypes :List<MovementType> { }
	public class ActionTypes :List<ActionType> { }
}

namespace MauronAlpha.MonoGame.Logic.Actions.DataObjects {
	using MauronAlpha.MonoGame;
	using MauronAlpha.MonoGame.Logic.Actions.DataObjects;
	using MauronAlpha.MonoGame.Logic.DataObjects;

	public abstract class ActionDescription :MonoGameComponent { 
	
	
	}
	public abstract class MovementType :ActionDescription { }
	public abstract class ActionType :MonoGameComponent {
		public abstract bool IsMovement { get; }
		public abstract GameValue<T_Noise> Noise { get; }
	}

	/// <summary> Noise Factor (Percent) </summary>
	public class T_Noise :ValueType {

		public override GameName Name { get { return new GameName("Noise"); } }

		public override BaseValueType BaseType { get { return VT_Percent.Instance; } }

	}
}
