using MauronAlpha.GameEngine;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace MauronAlpha.GameEngine.MonoGame {

	//Interface to MonoGame Framework
    public class MauronAlpha_MonoGame : MauronAlpha.GameEngine.Game {
		//constructor
		public MauronAlpha_MonoGame(Microsoft.Xna.Framework.Game monogameinstance, GameLogic gamelogic) : base(GameType_generic.Instance,gamelogic) {
			MonoGame = monogameinstance;
			monogame_graphicsdevice=MonoGame.GraphicsDevice;
			monogame_spritebatch=new SpriteBatch(monogame_graphicsdevice);
			GameLogic=gamelogic;

			//Base directory for content
			ContentLoader.RootDirectory=ContentDirectory;
			Start(gamelogic);
		}

		//The Name of the Game
        public override string Name { 
			get {
				return "{MonogameWrapper} "+GameLogic.Game.Name;
			}
		}

#region Monogame Assets

		//The Interface to draw stuff
		private SpriteBatch monogame_spritebatch;
        public SpriteBatch DrawEngine { 
			get {
				return monogame_spritebatch;
			}
		}
		
		//Information about the display etc
		private GraphicsDevice monogame_graphicsdevice;

        public GraphicsDevice GraphicsDevice {
			get {	return monogame_graphicsdevice;	}
		}
		
		//Content Manager
		public string ContentDirectory="Content";
		public ContentManager ContentLoader {
			get {	return MonoGame.Content;	}
		}
		
		//public
		private Microsoft.Xna.Framework.Game MonoGame;

		public virtual void ClearScreen(){
			Debug("screen cleared",this);
			GraphicsDevice.Clear(Color.CornflowerBlue);
		}

		public virtual void monogame_initialize() {
			Initialize();
			

		}
		public virtual void monogame_loadcontent() {
			GameLogic.Prepare();
		}
		public virtual void monogame_unloadcontent(){}
		public virtual void monogame_update(GameTime gametime) {
			if( DrawBuffer.Buffer.Count<1 ) {
				MonoGame.SuppressDraw();
			}
		}
		public virtual void monogame_draw(GameTime gametime) {
			//checking for buffer overruns
			if(DrawBuffer.Busy) {	Debug("Frame dropped", this); return;	}
			if(DrawBuffer.Buffer.Count<1){	Debug("Nothing to draw",this);	return;		}
			DrawBuffer.Busy=true;
			ClearScreen();

			//declare a nnew Stack of GameDrawData
			Stack<GameDrawData> drawdata=new Stack<GameDrawData>();
			while(DrawBuffer.Buffer.Count>0){
				Drawable drawable = DrawBuffer.Buffer.Pop();
				GameDrawData drawme=drawable.Draw(MonoGameConvert.GameTime2TimeSpan(gametime));
				drawable.GameDrawData=drawme;
				if(drawme==null) {
					throw new GameCodeError("DrawData for stack is Null",this);
				}
				drawdata.Push(drawme);
			}

			PerformDraw(drawdata,gametime);

			DrawBuffer.Busy=false;
		}
		//start drawing
		public void PerformDraw(Stack<GameDrawData> data, GameTime gametime){
			Debug(gametime.ElapsedGameTime+" : Performing draw action",this);

			//Prerender each Part
			while(data.Count>0){
				GameDrawData part=data.Pop();
				Debug(part.Source.Name,part);
				if(part==null){
					throw new GameCodeException("Part in Drawstack is null",this);
				}
				MonoGameRenderer.RenderPart(this, part);
			}
		}


#endregion		

#region MauronAlpha Assets
		
		/* Data Specific to this class */
        private DrawBuffer DrawBuffer = new DrawBuffer();
        private Texture2D TextureSolid;


#endregion

#region Methods

		//Initialize
        public void Start(GameLogic gamelogic){
            this.Self = new GameInstance(this);

			MauronCode.Debug("Started",this);

            Device device = new Device();
            OS os = new MonoGameOS();
            os.frontend = this.Self;
            os.backend = gamelogic;
			gamelogic.Initialize(this);
            
            base.Start(this.Self, device, os);
        }

		//Initialize all required functions
        public void Initialize() {
			MauronCode.Debug("Initializing", this);
			DrawBuffer = DrawBuffer.Instance;

			//Initialize the MauronAlpha specific stuff
			AssetManager.Initialize(this, new MonoGameAssetLoader());
			TextureManager.Initialize(this);

            //build draw Texture
            TextureSolid = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
            Color[] color = new Color[25];
            for (int i = 0; i < color.Length; i++) {
                color[i] = Color.White;
            }
            TextureSolid.SetData(color);
        }

		//Handling Callbacks
		public override void Callback (string message, object source) {
			Debug(message,this);
			if(message=="LoadQueueComplete"){
				GameLogic gamelogic = this.Os.backend as GameLogic;
				gamelogic.Callback(message,source);
			}
		}

#endregion

#region Abstracts that are not yet implemented
		public override object WindowSize {
			get {
				throw new System.NotImplementedException();
			}
			set {
				throw new System.NotImplementedException();
			}
		}
		public override GameComponent Graphics {
			get {
				throw new System.NotImplementedException();
			}
			set {
				throw new System.NotImplementedException();
			}
		}
		public override GameComponent Sound {
			get {
				throw new System.NotImplementedException();
			}
			set {
				throw new System.NotImplementedException();
			}
		}
		public override void Load (SaveGame savegame) {
			throw new System.NotImplementedException();
		}
		public override void DeleteSaveGame (SaveGame savegame) {
			throw new System.NotImplementedException();
		}
		public override void Save (SaveGameData savegamedata) {
			throw new System.NotImplementedException();
		}
		public override void End ( ) {
			throw new System.NotImplementedException();
		}
#endregion

	}

	public class MonoGameInstance:Microsoft.Xna.Framework.Game {
	}
}

