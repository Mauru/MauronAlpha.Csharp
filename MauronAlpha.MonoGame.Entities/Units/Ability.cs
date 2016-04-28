using MauronAlpha.MonoGame.Quantifiers.Units;
using MauronAlpha.MonoGame.Entities.Collections;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Ability:EntityComponent {

		EntityValue<T_Duration> WindUpTime;
		Modifiers WindUpEffects;

		EntityValue<T_Duration> Duration;
		Modifiers Effects;

		EntityValue<T_Duration> WindDownTime;
		Modifiers AfterEffects;

	}

}
