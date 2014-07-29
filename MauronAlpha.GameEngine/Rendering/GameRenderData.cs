using MauronAlpha.GameEngine.Events;
using MauronAlpha.GameEngine.Rendering.Events;

using System.Collections.Generic;
using System;

namespace MauronAlpha.GameEngine.Rendering {
	
	//A dataset for holding info required for drawing
	public class GameRenderData : MauronCode_dataobject, I_GameEventListener,I_GameEventSender {

		#region What is the source object of this dataobject? - i.e. Which Graphical Entitiy (I_Drawable)
		internal I_Drawable D_source=null;
		public I_Drawable Source {	
			get { return D_source; } 
			set { throw new GameCodeError("Use SetSource() Instead",this); }
		}
		private void SetSource (I_Drawable source ) { D_source = source; }
		public bool HasSource { get { return (D_source!=null); } }
#endregion
	
		#region The Rendershedule and timing

		#region What is my renderShedule?
		private GameRenderShedule GRS_renderShedule;
		public GameRenderShedule RenderShedule { 
			get {
				if(GRS_renderShedule==null){
					throw new GameCodeError("RenderShedule is null!",this);
				}
				return GRS_renderShedule;
			}
		}
		#endregion

		#region Here is your new shedule
		private void SetRenderShedule(GameRenderShedule shedule){
			GRS_renderShedule=shedule;
		}
		#endregion

		#region When were you last Sheduled to be rendered?
		public GameTick LastSheduled { get { return RenderShedule.Sheduled; } }
		#endregion

		#region When was the object last rendered?
		GameTick TS_lastRendered=new GameTick(GameMasterClock.Instance.RenderClock, GameMasterClock.Instance.RenderClock.Time);
		public GameTick LastRendered { get { return TS_lastRendered; } }
		private void SetLastRendered ( ) { TS_lastRendered=RenderClock.Time; }
		#endregion

		#region Are you sheduled to be rendered?
		public bool IsSheduled {
			get {
				// Ask the Render Shedule!
				return RenderShedule.IsSheduled;
			}
		}
		#endregion

		#region What is your RenderClock?
		internal GameEventClock RenderClock {
			get {
				return GameMasterClock.Instance.RenderClock;
			}
		}
		#endregion?

		#region Add yourself to the Shedule!
		public void SheduleRender ( ) {
			if( !IsSheduled ) {
				GameRenderBuffer.Instance.Add(Source);
			}
		}
		#endregion

		#region What is the time of your RenderClock?
		internal GameTick RenderTime {
			get {
				return RenderClock.Time;
			}
		}
		#endregion

#endregion		
		
		#region Verifying and Setting of the Resulting image

		#region Give me your Result data!
		private object O_result=null;
		public object Result {
			get {
				if(Result==null){
					throw new GameCodeError("Renderdata has no Result!",this);
				}
				return O_result;
			}
		}
		#endregion

		#region This is your result
		public GameTick SetResult(object result) {
			if(result==null) {
				throw new GameCodeError("Result can not be null",this);
			}
			O_result=result;
			SetLastRendered();
			return GameMasterClock.Instance.RenderClock.Time;
		}
		#endregion 

		#region Do you have a result?
		public bool HasResult { get { return O_result!=null; } }
		#endregion

		#region Do I need to be re-rendered?
		public bool NeedsRenderUpdate {
			get {
				return (HasResult&&(LastRendered.CompareTo(0)>0)&&RenderShedule.NeedsUpdate(RenderShedule));
			}
		}
		#endregion

#endregion

		public void E_checkDataStatus(GameRenderData renderData) {}
		public void E_checkRenderStatus (GameRenderData renderData) {}


		//Constructor
		public GameRenderData (I_Drawable source, GameRenderShedule renderShedule )	: base() {
			SetSource(source);
			SetRenderShedule(renderShedule);			
		}

		#region I_GameEventSender
		public GameEventWatcher GameEventWatcher {
			get { throw new NotImplementedException(); }
		}

		public void SendEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public GameEventShedule EventShedule {
			get { throw new NotImplementedException(); }
		}
		public bool CheckEvent (I_GameEventSender d) {
			throw new System.NotImplementedException();
		}
		#endregion

		#region I_GameEventListener
		public void ReceiveEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public void ReceiveEvents (GameEvent[] a_ge) {
			foreach( GameEvent ge in a_ge ) {
				ReceiveEvent(ge);
			}
		}
		public void IsEventCondition (GameEvent ge) {
			throw new NotImplementedException();
		}
		#endregion
	}

}