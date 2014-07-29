using MauronAlpha.Geometry._2d;
using MauronAlpha.GameEngine.Positioning;
using MauronAlpha.GameEngine.Rendering.Textures;
using MauronAlpha.GameEngine.Rendering.Events;
using MauronAlpha.GameEngine.Events;


using System;
using System.Collections.Generic;

namespace MauronAlpha.GameEngine.Rendering {

	//Blueprint for a GameElement which can be placed and drawn
	public abstract class Drawable : GameComponent, I_Drawable {

		//constructor
		public Drawable(DrawableType dt, I_Drawable parent, string name) {
			DT_drawableType=dt;
			STR_name=name;
			
			//Event Sheduling
			InitializeEventWatchers();
		}

		#region I_Drawable

		// Give me your Name!
		internal readonly string STR_name;
		public string Name {
			get { return STR_name; }
		}
		
		// What can You do ? Give me a description!
		internal readonly DrawableType DT_drawableType;
		public DrawableType DrawableType {
			get { return DT_drawableType; }
		}

		// Should You be Drawn?
		internal bool B_visible=true;
		public virtual bool Visible {
			get { return B_visible; }
		}

		// Are you actually visible? MAKE SURE!
		public virtual bool IsVisible() {
			return HasRendered;
		}

		// Have I been rendered at least once?
		public bool HasRendered {
			get { return (RenderData!=null&&RenderData.HasResult); }
		}
		
		// Do I have to be rendered?
		public bool NeedsRendering {
			get { return (RenderData==null||RenderData.NeedsRenderUpdate); }
		}

		// Am I sheduled to be rendered?
		public bool RenderSheduled { get { return RenderData.IsSheduled; } }

		// Shedule me to be rendered!
		public virtual void SheduleRender ( ) { 
			if(RenderData==null) {
				throw new GameCodeError("I Dont have RenderInstructions!",this);
			}
			RenderData.SheduleRender(); 
		}

		// When am I supposed to be rendered and how often?
		internal GameEventShedule GES_renderShedule;
		public virtual GameEventShedule RenderShedule { 
			get {
				if(GES_renderShedule==null) {
					throw new GameCodeError("No RenderShedule set!",this);
				}
				return GES_renderShedule;
			}
		}

		// Define My Instruction Set!
		public abstract void GenerateRenderData ();

		// What is my Instruction Set ?
		GameRenderData GRD_renderData=null;
		public GameRenderData RenderData {
			get {
				if( RenderData==null ) {
					throw new GameCodeError("RenderData can't be null!", this);
				}
				return GRD_renderData;
			}
			set {
				throw new NotImplementedException();
			}
		}

#region Something has happened with me! React to it!
		//I have been created!
		public delegate void DEL_setAsSpawned();

		public virtual void Callback(GameEvent e) {
		}

		public static event DEL_setAsSpawned EVENT_spawned;
		public static bool DEL_CheckEvent(I_GameEventSender d) {
			return d.CheckEvent(d);
		}
		
		public void EVENT_setAsSpawned(I_Drawable d) {
			d.E_setAsSpawned();
		}

		public virtual void E_setAsSpawned(){
			// Do I have Renderdata?
			if(RenderData==null) {
				// no. Create it!
				GenerateRenderData();
			}

			// Ask the renderShedule if I should be rendered at the start
			if(RenderData.RenderShedule.RenderAtStart) {
				// yes, render me
				RenderData.SheduleRender();

			}
		}
#endregion

#endregion		

		#region I_Positionable 

		private Rectangle2d R_bounds;
		public virtual Rectangle2d Bounds { get { return R_bounds; } }
		public virtual void SetBounds (Rectangle2d rect) {
			 R_bounds=rect;
		}

		private Vector2d V2_position;
		public void SetPosition (Vector2d v) {
			Bounds.SetPosition(v);
		}
		public Vector2d Position {
			get { return Bounds.Points[0]; }
		}



#endregion

		#region I_RenderObjectParent
		public I_Drawable LastChild { 
			get { 
				if(Children.Length<1) {
					throw new GameCodeError("Item does not have any children",this);
				}
				return Children[Children.Length-1];
			} 
		}

		public I_Drawable FirstChild {
			get { throw new NotImplementedException(); }
		}


		public I_Drawable[] Children {
			get { throw new NotImplementedException(); }
		}

		public void AddChild (I_Drawable child) {
			throw new NotImplementedException();
		}

		public void RemoveChild (I_Drawable child) {
			throw new NotImplementedException();
		}
		#endregion

		#region I_RenderObjectChild

		//Parent
		internal I_Drawable D_parent=null;
		public I_Drawable Parent {
			get {
				if( D_parent==null ) {
					throw new GameCodeError("Item does not have a parent", this);
				}
				return D_parent;
			}
		}

#endregion

		#region I_GameEventListener 
		public virtual void ReceiveEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public virtual void ReceiveEvents(GameEvent[] a_ge) {
			foreach(GameEvent ge in a_ge){
				ReceiveEvent(ge);
			}
		}
		public void IsEventCondition (GameEvent ge) {
			throw new NotImplementedException();
		}
#endregion

		#region I_GameEventSender

		//send an event
		public virtual void SendEvent (GameEvent ge) {
			throw new NotImplementedException();
		}

		//Check for an event
		public virtual bool CheckEvent (I_GameEventSender d) {
			throw new NotImplementedException();
		}

		// Return the item even shedule
		internal GameEventShedule GES_eventShedule;
		public virtual GameEventShedule EventShedule { get { return GES_renderShedule; } }

		// Initialize event watchers
		GameEventWatcher GEW_event; // The watcher for events
		GameRenderWatcher GRW_render; // The watcher for render events
		internal virtual void InitializeEventWatchers ( ) {
			//we are setting a Default trigger-shedule 
			// (might want to get a default from a centralized class in the future)
			GRW_render = new GameRenderWatcher(this);
			GEW_event = new GameEventWatcher(this);
		}
		public virtual GameEventWatcher GameEventWatcher { get { return GEW_event; } }

#endregion

	}
}