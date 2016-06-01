using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;

namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {
	
	public interface I_Habitable {
		GameLocation Location { get; }

		MoveData MovementDataFor(I_Movable obj);

		bool IsBeing { get; }
		bool IsAttached { get; }

	}

}
