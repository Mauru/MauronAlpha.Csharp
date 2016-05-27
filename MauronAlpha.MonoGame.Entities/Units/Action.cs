using MauronAlpha.MonoGame.Entities.Quantifiers;
using MauronAlpha.MonoGame.Quantifiers;

namespace MauronAlpha.MonoGame.Entities.Units {
	public abstract class Action {

		public abstract string Name { get; }

		public T_Duration Duration;
		public T_Duration WindUp;
		public T_Duration CoolDown;
	}
}
