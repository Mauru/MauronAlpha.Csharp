using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Utility;

using MauronAlpha.MonoGameEngine.Collections;
using MauronAlpha.MonoGame.Test;

namespace MauronAlpha.MonoGameEngine {


	public class MonoGameWrapper : Microsoft.Xna.Framework.Game {
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		GraphicsDevice device;
		Effect effect;

		ShapeBuffer Buffer = new ShapeBuffer();
		VertexBuffer RenderBuffer;

		public MonoGameWrapper() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize() {
			graphics.PreferredBackBufferWidth = 500;
			graphics.PreferredBackBufferHeight = 500;
			graphics.IsFullScreen = false;
			graphics.ApplyChanges();
			Window.Title = "Testcases for the monogame engine";

			base.Initialize();
		}

		protected override void LoadContent() {
			//Loading content
			spriteBatch = new SpriteBatch(GraphicsDevice);
			device = graphics.GraphicsDevice;
			effect = Content.Load<Effect>("default");

			//Create a test polygon
			Rectangle2d shape = new Rectangle2d(0,0,20,20);
			Triangulator2d triangulator = new Triangulator2d();
			Buffer.Add(new TriangleList(triangulator.Triangulate(shape)));
			VertexPositionColor[] vertices = new VertexPositionColor[0];
			foreach (TriangleList list in Buffer) {
				 vertices = list.PrepareRender();
			}

			int vCount = vertices.Length;

			RenderBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
			RenderBuffer.SetData<VertexPositionColor>(vertices);
			
		}

		protected override void UnloadContent() {
		}

		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {

			device.Clear(Color.DarkSlateBlue);
			/*TestTriangle tri = new TestTriangle();
			tri.Setup();*/


			effect.CurrentTechnique = effect.Techniques["Pretransformed"];

			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = CullMode.None;
			GraphicsDevice.RasterizerState = rasterizerState;

			foreach (EffectPass pass in effect.CurrentTechnique.Passes) { 
				pass.Apply();

			}
			//device.DrawUserPrimitives(PrimitiveType.TriangleList, tri.Vertices, 0, 1, VertexPositionColor.VertexDeclaration);


			base.Draw(gameTime);
		}
	
	
	}
}
