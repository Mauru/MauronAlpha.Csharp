using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Concepts.Units {
	
	public class Potion:Item {

		T_Time WindUpTime;
		Modifiers WindUpModfiers;

		T_Time Duration;
		Modifiers Effects;

		T_Time WindDownTime;
		Modifiers AfterEffects;

	}

}
