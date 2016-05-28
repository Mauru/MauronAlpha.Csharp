using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using MauronAlpha.MonoGame.Actors;

using MauronAlpha.MonoGame.DataObjects;
using MauronAlpha.Events.Units;

namespace MauronAlpha.MonoGame.Utility {

	/*Renderer*/
	public class RenderManager:MonoGameComponent {
		private Stack<RenderRequest> RenderRequests = new Stack<RenderRequest>();
		private Stack<Texture2D> Sprites = new Stack<Texture2D>();

		private GameTime LastUpdate = new GameTime();
		private GameTime LastRender = new GameTime();

		public EventUnit_timeStamp TimeStamp {
			get {
				return new EventUnit_timeStamp(RenderSteps.Steps);
			}
		}

		EventUnit_counter RenderSteps = new EventUnit_counter();

		public long CurrentStep { get { return RenderSteps.Steps; } }

		private GraphicsDevice Device;
		SpriteBatch SpriteBatch;

		public RenderManager(GraphicsDevice device):base() {
			Device = device;
		}

		public RenderManager RenderStep(GameTime time) {
			RenderSteps.Step();
			SpriteBatch = new SpriteBatch(Device);
			SpriteBatch.Begin();

			while(RenderRequests.Count > 0)
				Render(RenderRequests.Pop());

			SpriteBatch.End();

			return this;
		}
		public RenderManager SpriteStep(GameTime time) {
			return this;
		}
		public RenderManager AddRequest(RenderLevel renderLevel, RenderInstructions instructions) {
			RenderRequest request = new RenderRequest(renderLevel,instructions, TimeStamp);
			RenderRequests.Push(request);
			return this;
		}

		public void Render(RenderRequest request) {
			if (request.IsEmpty)
				return;
			GameActor actor = request.Actor;
			RenderInstructions instructions = actor.RenderInstructions;
			RenderLevel level = actor.R
		}
	}
}
