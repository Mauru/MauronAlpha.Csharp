using System;
using System.IO;
using System.Collections.Generic;

namespace MauronAlpha.GameEngine {
	
	//Base class for generated and imported Assets (data sets, files)
	public class GameResource:GameComponent {}

	//Base class for all data bound assets (XMLs, etc)
	public abstract class DataResource:GameAsset {
		public object Data;

		//constructros
		public DataResource(string name, string filename):base(GameAssetType_data.Instance,name,filename){}
		public DataResource():base(GameAssetType_data.Instance){}
		
		//Loading data
		public void Load(Stream stream){
			Data=Parse(stream);
		}

		//parsing data from a stream
		public abstract object Parse(Stream stream);
	}

	//a component that needs to be loaded (Textures, Files)
	public class GameAsset:GameResource {
		public string Name;
		public string FileName;
		public GameAssetType AssetType;

		//Load related
		public bool Loaded=false;
		public object Result;
		public DataResource Handler;
		public Type ResultType;

		//constructors
		public GameAsset(GameAssetType assettype){
			AssetType=assettype;
		}
		public GameAsset (GameAssetType assettype, string name,string filename) {
			AssetType=assettype;
			Name=name;
			FileName=filename;
		}
		public GameAsset (GameAssetType assettype, string name,string filename, DataResource handler) {
			AssetType=assettype;
			Name=name;
			FileName=filename;
			Handler=handler;
		}
	}

	public class GameAsset_savegame:GameAsset {
		public GameAsset_savegame():base(GameAssetType_data.Instance){}
	}

	//Base class of Asset Type descriptions
	public abstract class GameAssetType:MauronCode_subtype {
		public abstract string Name{get;}
	}

	//Asset Types
	public class GameAssetType_image:GameAssetType {
		#region Singleton
		private static volatile GameAssetType_image instance=new GameAssetType_image();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static GameAssetType_image ( ) { }
		public static GameAssetType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new GameAssetType_image();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {get{ return "image";}}
	}
	public class GameAssetType_data : GameAssetType {
		#region Singleton
		private static volatile GameAssetType_data instance=new GameAssetType_data();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static GameAssetType_data ( ) { }
		public static GameAssetType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new GameAssetType_data();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "data"; } }
	}

	//Baseclass for a list of Assets
	public abstract class GameAssetList:GameComponent {
		public abstract Stack<GameAsset> List {	get;}
	}

}
