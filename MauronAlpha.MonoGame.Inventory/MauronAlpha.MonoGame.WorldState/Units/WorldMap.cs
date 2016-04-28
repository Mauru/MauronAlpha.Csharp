using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.HexGrid.Units;
using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.MonoGame.WorldState.Units {
	
	public class WorldMap:WorldStateComponent {
		Grid Grid;
		Vector2d Size;
		
		public WorldMap(Vector2d size):base() { 
			Size = size;
		}

		public void Populate() {
			Grid = new Grid(Size);

			Hex start = Grid.Hex(0, 0, 0);

			GenerateEnvironment(start);
			

		}

		public void GenerateEnvironment(Hex hex) {

		}


	}
}
