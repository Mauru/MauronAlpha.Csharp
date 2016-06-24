using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Actuals;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

using MauronAlpha.MonoGame.HexGrid.Units;

namespace MauronAlpha.MonoGame.SpaceGame.Utility {

	public class MapGenerator : GameComponent {

		GameRules Rules;
		GameState State;

		Map Map;
		Universe Universe;

		public void Start(GameRules rules, GameState state) {

			Rules = rules;
			State = state;


		}


	}


}

//Minerals - 
//Energy
//Culture
//Science
//Food
