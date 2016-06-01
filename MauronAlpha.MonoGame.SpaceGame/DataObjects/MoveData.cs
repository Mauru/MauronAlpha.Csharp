using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Units;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	
	public class MoveData:GameComponent {

		public GameLocation Start;
		public GameLocation End;
		public MovementType Type;
		public I_Movable Actor;

	}

	public class MovementType : GameComponent { }

	public interface I_Movable: I_Being { }

}
