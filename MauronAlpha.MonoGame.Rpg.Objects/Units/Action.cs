using MauronAlpha.MonoGame.Quantifiers.Units;
using MauronAlpha.MonoGame.Entities.Collections;

namespace MauronAlpha.MonoGame.Concepts.Units {
	
	public class Action:GameConcept {

		EntityValue<T_Duration> WindUp;
		EntityValue<T_Duration> Duration;
		EntityValue<T_Duration> Recovery;

		public readonly EntityValue<T_Time> TimeStarted = new EntityValue<T_Time>(true);

		Modifiers OnWindUp;
		Modifiers OnPerform;
		Modifiers OnRecovery;

		public void Perform(EntityValue<T_Time> time) {

		}

	}

}
