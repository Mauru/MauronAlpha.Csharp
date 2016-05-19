using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.WorldGenerator.DataObjects;

namespace MauronAlpha.MonoGame.WorldGenerator.Units {
	
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


			BluePrint.CreateGeography(start,this);
			BluePrint.CreateBasicHistory(start,this);
		
			

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
