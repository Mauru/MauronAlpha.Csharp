using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Quantifiers.Units;
using MauronAlpha.MonoGame.Concepts.Quantifiers;

namespace MauronAlpha.MonoGame.Concepts.Units {
	
	public class SpellComponent:GameConcept {

		EntityValue<T_Duration> LearningTime;

		EntityValue<T_TargetCount> TargetsTotal;

		EntityValue<T_TargetCount> TargetsAllied;

		EntityValue<T_TargetCount> TargetsEnemy;

		EntityValue<T_Range> Range;

		EntityValue<T_Duration> CastTime;

		Modifiers OnInitiateSelf;
		Modifiers OnCastEnemy;
		Modifiers AfterEffectsEnemy;
		Modifiers AfterEffectsSelf;
		Modifiers AfterEffectsAllied;

	}
}
