using System;


namespace MauronAlpha.GameEngine.Rendering {

	//Drawable: Has Children, has several instances of content
	public abstract class Drawable_animated : Drawable {
		public Drawable_animated (Drawable parent, string name)
			: base(DrawableType_animated.Instance, parent, name) {
		}
	}
	public sealed class DrawableType_animated : DrawableType {
		#region Singleton Definition
		private static volatile DrawableType_animated instance=new DrawableType_animated();
		private static object syncRoot=new Object();
		static DrawableType_animated ( ) { }
		public static DrawableType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DrawableType_animated();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name { get { return "animated"; } }
	}

}
