using MauronAlpha.MonoGame.Actors;
using MauronAlpha.Events.Units;
using MauronAlpha.MonoGame.Resources;

namespace MauronAlpha.MonoGame.DataObjects {
	public class RenderRequest:MonoGameComponent {
		RenderInstructions DATA_instructions;
		RenderInstructions Instructions { get { return DATA_instructions; } }
		EventUnit_timeStamp Time;
		RenderLevel Level;

		EventUnit_timeStamp TIME_lastRender;

		public RenderRequest(RenderLevel level, RenderInstructions instructions, EventUnit_timeStamp time):base() {
			DATA_instructions = instructions;
			Level = level;
			Time = time;
		}

		public EventUnit_timeStamp LastRendered { get {
			if (TIME_lastRender == null)
				return new EventUnit_timeStamp(0);
			return TIME_lastRender;
		} }

		public GameTexture Result;

		public bool IsEmpty {
			get {
				return Level.IsEmpty;
			}
		}

		public GameActor Actor { get { return Level.Actor; } }
	}
}
