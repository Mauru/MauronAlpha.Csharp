using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Planes:EntityComponent {
		MauronCode_dataMap<Plane> Data;

		WorldClock Clock;

		public Planes(WorldClock clock)	: base() {
			Clock = clock;
		}
		
		public void Set(string name, Plane plane) {
			Data.SetValue(name, plane);
		}

		public Plane ByName(string name) {
			Plane result = null;
			if (!Data.TryGet(name, ref result))
				Data.SetValue(name, new Plane(name,Clock));
			return Data.Value(name);
		}

		public Plane Create(string name) {
			Plane p = new Plane(name, Clock);
			Set(name,p);
			return p;
		}


	}
}
