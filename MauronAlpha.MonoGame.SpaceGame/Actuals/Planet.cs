﻿using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {

	public class Planet : Orbital<OT_habitable>, I_Habitable  {
		public new GameName Name;
		public Planet(GameName name, GameLocation location, GameValue<T_PlanetSize> size, GameValue<T_DistanceFromStarCentre> distance)
			: base(location) {
			Name = name;
			Size = size;
			DistanceFromStarCentre = distance;
		}

		public GameValue<T_PlanetSize> Size;
		public GameValue<T_DistanceFromStarCentre> DistanceFromStarCentre;

		public DataObjects.MoveData MovementDataFor(DataObjects.I_Movable obj) {
			throw new System.NotImplementedException();
		}

		public GameLocation Location {
			get { throw new System.NotImplementedException(); }
		}

		public bool IsAttached {
			get { return true; }
		}
	}

	public class OT_Planet : OT_habitable { }

	public abstract class GeoType : GameComponent {}

	//Determines the state of the ground
	public class ValueType_geoActivity : T_Percent {

		//nonsolid - the planet is either made of gas or molten 0
		//shifting - the planet has solid ground, but it randomly shifts 50
		//100 - totally solid, everything is made of one material no warmth no geodiversity

	}
	public class ValueType_atmosphereDensity : T_Percent {

		//no atmostphere at all
		//Traces of an atmospheric gas exist
		//The atmosphere is so "Full" you can not reach the ground

	}
	public class ValueType_atmosphereActivity : T_Percent {

		//calm, unchanging
		//Traces of an atmospheric gas exist
		//Violently stormy

	}
	public class ValueType_geoDiversity : T_Percent {

		//How many different "layers" a planet has

	}
	public class ValueType_geoHeight : T_Percent {

		//0 = Deep Chasms
		//50 = perfectly level to the core
		//100 = HighMountains
	
	}
	public class ValueType_geoType: T_Percent {
		//0 gas
		//2 liquid
		//3 solid
	}
	public class ValueType_geoTemperature : T_Percent { }
}
