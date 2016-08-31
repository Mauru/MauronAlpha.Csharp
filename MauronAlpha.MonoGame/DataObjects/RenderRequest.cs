using MauronAlpha.Events.Units;
using MauronAlpha.MonoGame.Resources;

namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Collections;

	public class RenderRequest:MonoGameComponent {
		RenderInstructions DATA_instructions;
		RenderInstructions Instructions { get { return DATA_instructions; } }

		long Time;
		long Finished = 0;
		MonoGameTexture Result;

		public readonly Drawable Target;

		EventUnit_timeStamp TIME_lastRender;

		public RenderRequest(Drawable target, RenderInstructions instructions, long time):base() {
			DATA_instructions = instructions;
			Target = target;
			Time = time;
		}
	}
}
