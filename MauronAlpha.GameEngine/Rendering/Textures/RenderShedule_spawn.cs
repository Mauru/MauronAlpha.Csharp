using System;
using MauronAlpha.GameEngine.Events;

namespace MauronAlpha.GameEngine.Rendering.Events {
	
	//A shedule which runs once automatically, then manually
	public class RenderShedule_spawn : GameRenderShedule {
		//define delegate
		public delegate void DELEGATE (I_Drawable source);

		//constructor
		public RenderShedule_spawn (I_Drawable source, DELEGATE action)
		: base(
			SheduleType_spawn.Instance,
			source,
			MakeDelegate(source)
		) {}

		//delegate event function - executed after source has spawned
		private static DELEGATE MakeDelegate(I_Drawable source){
			return n => {	source.RenderData.SheduleRender();  };
		}
	
	}

	//Description of this shedule
	public sealed class SheduleType_spawn : GameEventSheduleType {
		#region Singleton
		private static volatile SheduleType_spawn instance=new SheduleType_spawn();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static SheduleType_spawn ( ) { }
		public static GameEventSheduleType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new SheduleType_spawn();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name {
			get { return "Spawn"; }
		}
	}
}
