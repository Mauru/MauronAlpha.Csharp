#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;

using MauronAlpha;
using MauronAlpha.Games.PixelRPG;
#endregion

namespace MauronAlpha.GameEngine.MonoGame.PixelRPG {

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PixelRPG_MonoGame :	Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;

		//MauronAlpha.GameEngine.GameLogic ( The game we are loading )

		//interface between MauronAlpha.GameEngine and MonoGame Framework
		MauronAlpha_MonoGame Mauron_MonoGame;		
		public PixelRPG_MonoGame() : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize(){
            // TODO: Add your initialization logic here
			Mauron_MonoGame=new MauronAlpha_MonoGame(this,new PixelRPG_GameLogic());
			Mauron_MonoGame.monogame_initialize();
			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent(){
            // Create a new SpriteBatch, which can be used to draw textures.
			Mauron_MonoGame.monogame_loadcontent();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent(){

            Mauron_MonoGame.monogame_unloadcontent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
			Mauron_MonoGame.monogame_update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime){
			Mauron_MonoGame.monogame_draw(gameTime);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }

}