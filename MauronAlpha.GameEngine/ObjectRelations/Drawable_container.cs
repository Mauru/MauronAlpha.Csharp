using MauronAlpha.GameEngine.Rendering;
using System;

namespace MauronAlpha.GameEngine.ObjectRelations {

	//Drawable: has no content, has children
	public abstract class Drawable_container : Drawable {
		public Drawable_container (I_Drawable parent, string name)
			: base(DrawableType_container.Instance,parent, name) {
		}
	}
	public sealed class DrawableType_container : DrawableType {
		#region Singleton Definition
		private static volatile DrawableType_container instance=new DrawableType_container();
		private static object syncRoot=new Object();
		static DrawableType_container ( ) { }
		public static DrawableType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DrawableType_container();
					}
				}
				return instance;
			}
		}
		#endregion
		public override bool IsDrawParent { get { return true; } }
		public override string Name { get { return "container"; } }
	}

}
