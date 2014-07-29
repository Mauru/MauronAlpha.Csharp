using System;

using MauronAlpha.GameEngine.Positioning;
using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Rendering {

	//Drawable: has no parent
	public class Drawable_layer : Drawable {
		//constructor
		public MauronAlphaGame Source;
		public Drawable_layer (MauronAlphaGame parent, string name):base(DrawableType_layer.Instance,null,name) {}

		public override void GenerateRenderData ( ) {
			throw new NotImplementedException();
		}

		public override Rectangle2d Bounds {
			get { throw new NotImplementedException(); }
		}

		public override GameEngine.Events.GameEventShedule RenderShedule {
			get { throw new NotImplementedException(); }
		}
	}
	public sealed class DrawableType_layer : DrawableType {
		#region Singleton Definition
		private static volatile DrawableType_layer instance=new DrawableType_layer();
		private static object syncRoot=new Object();
		static DrawableType_layer ( ) { }
		public static DrawableType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DrawableType_layer();
					}
				}
				return instance;
			}
		}
		#endregion
		public override bool IsDrawLayer { get { return false; } }
		public override bool IsDrawParent { get { return true; } }
		public override string Name { get { return "layer"; } }
	}

}
