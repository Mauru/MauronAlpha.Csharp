using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MauronAlpha.MonoGame.Utility;
using MauronAlpha.MonoGame.Setup;
using MauronAlpha.MonoGame.Scripts;

using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.MonoGame {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class MonoGameWrapper : Game {

		GameEngine Engine;
		public RenderManager Renderer;
		//Monogame related
		GraphicsDeviceManager graphics;
		
		//BasicEffect basicEffect;

		//SpriteBatch spriteBatch;
		//VertexBuffer vertexBuffer;


		Matrix world = Matrix.CreateTranslation(0, 0, 0);
		Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
		Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(75), 800f / 480f, 0.01f, 100f);

		//Constructor
		public MonoGameWrapper(GameLogic logic):base() {
			//Initialize graphics
			graphics = new GraphicsDeviceManager(this);
			//Set Content Directory
			Content.RootDirectory = "Content";
			//Set up Engine
			Engine = new GameEngine(this,logic);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			
			//Set up Renderer
			Renderer = new RenderManager(GraphicsDevice);



			//Do MonoGame Initialization
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {

			Engine.InitializeContent();

			// Create a new SpriteBatch, which can be used to draw textures.
			//spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			//basicEffect = new BasicEffect(GraphicsDevice);

			/*VertexPositionColor[] vertices = new VertexPositionColor[3];
			vertices[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);
			vertices[1] = new VertexPositionColor(new Vector3(+0.5f, 0, 0), Color.Green);
			vertices[2] = new VertexPositionColor(new Vector3(-0.5f, 0, 0), Color.Blue);
			vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);*/

			/*
			ShapeBuilder Builder = new ShapeBuilder(GraphicsDevice);

			vertexBuffer = Builder.ToBuffer(GameObjects.HexShape);*/
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
				|| Keyboard.GetState().IsKeyDown(Keys.Escape)
			)
				Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			/*
			basicEffect.World = world;
			basicEffect.View = view;
			basicEffect.Projection = projection;
			basicEffect.VertexColorEnabled = true;
			*/

			//GraphicsDevice.SetVertexBuffer(vertexBuffer);

			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = CullMode.None;
			GraphicsDevice.RasterizerState = rasterizerState;

			/*
			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes) {
				pass.Apply();
				//GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexBuffer.VertexCount);

				//Obj.DrawHex(spriteBatch, GraphicsDevice);
					Renderer.RenderStep(gameTime);
			}*/


			//Obj.DrawTestObject(spriteBatch, GraphicsDevice);

			Renderer.SpriteStep(gameTime);



			base.Draw(gameTime);
		}

		public Vector2d WindowSize {
			get {
				Rectangle bounds = base.Window.ClientBounds;
				return new Vector2d(bounds.Width, bounds.Height);
			}
		}

	}
}
