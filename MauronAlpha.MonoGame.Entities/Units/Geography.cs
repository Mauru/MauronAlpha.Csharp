using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	
	public class Geography:EntityComponent {

		Modifiers Modifiers;
		GeoValue Elevation;
		GeoValue Vegetation;
		GeoValue Rain;
		GeoValue Heat;

	}

	public class GeoValue : EntityValue<T_Percent> {}



}
