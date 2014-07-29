using System.Collections.Generic;
using MauronAlpha;
using MauronAlpha.Geometry;

namespace MauronAlpha.GameEngine {

	//a drawable object in a game
	public class GameObject : GameComponent { }

	//A Piece of code belonging to the game
    public class GameComponent : MauronCode_dataobject {
		public GameComponent():base(){}
	}
}