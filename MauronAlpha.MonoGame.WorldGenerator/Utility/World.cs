using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.DataObjects;
using MauronAlpha.MonoGame.Entities.Collections;

namespace MauronAlpha.MonoGame.WorldGenerator.Utility {
	
	public class World {

		public Planes Planes;

		public WorldClock Clock = new WorldClock();

		public WorldBluePrint BluePrint; 

		public void Generate(WorldBluePrint bluePrint) {
			BluePrint = bluePrint; 

			Planes = new Planes(Clock);
			Planes.Create("SpiritRealm");
			Planes.Create("Reality");
			Planes.Create("Limbo");
			Planes.Create("Start");

			//Generate Starting Location
			Site start = Start.NewSite;


			BluePrint.CreateGeography(start);
		
			

		}

		public Plane Limbo {
			get {
				return Planes.ByName("Limbo");
			}
		}

		public Plane Start {
			get {
				return Planes.ByName("Start");
			}
		}
	}
}
