using System;

using MauronAlpha.GameEngine.Rendering.Events;
using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Rendering {
	
	//A container that does not update unless specifically told to
	public sealed class DrawableType_static : DrawableType {
		#region Singleton Definition
		private static volatile DrawableType_static instance=new DrawableType_static();
		private static object syncRoot=new Object();
		static DrawableType_static ( ) { }
		public static DrawableType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DrawableType_static();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name { get { return "static"; } }
	}

	public class Drawable_static : Drawable {
		public override void GenerateRenderData ( ) {
			throw new NotImplementedException();
		}

		public Drawable_static(I_Drawable parent, string name):base(DrawableType_static.Instance,parent,name) {}

		public override Rectangle2d Bounds {
			get { throw new NotImplementedException(); }
		}

		public override GameEngine.Events.GameEventShedule RenderShedule {
			get { 
				return new RenderShedule_spawn(this,n=>{this.RenderData.SheduleRender();});
			}
		}
	}

}
