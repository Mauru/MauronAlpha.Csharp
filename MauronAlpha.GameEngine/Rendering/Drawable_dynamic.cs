using System;
using MauronAlpha.GameEngine;
using MauronAlpha.Geometry._2d;
using MauronAlpha.GameEngine.Events;


namespace MauronAlpha.GameEngine.Rendering {

	//Drawable: Has Children, updates every drawcall
	public class Drawable_dynamic : Drawable {

		public Drawable_dynamic (I_Drawable parent, string name)
		: base(DrawableType_dynamic.Instance,parent,name) {}

		public override void GenerateRenderData ( ) {
			throw new NotImplementedException();
		}

		public override Rectangle2d Bounds {
			get { throw new NotImplementedException(); }
		}

		public override GameEventShedule RenderShedule {
			get { throw new NotImplementedException(); }
		}
	}

	public sealed class DrawableType_dynamic : DrawableType {
		#region Singleton Definition
		private static volatile DrawableType_dynamic instance=new DrawableType_dynamic();
		private static object syncRoot=new Object();
		static DrawableType_dynamic ( ) { }
		public static DrawableType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DrawableType_dynamic();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name { get { return "dynamic"; } }
	}

}
