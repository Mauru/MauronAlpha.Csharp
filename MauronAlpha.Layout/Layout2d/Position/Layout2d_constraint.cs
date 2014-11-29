using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.Layout.Layout2d.Position {

	//Determines limits on size
	public class Layout2d_constraint:Layout2d_component{

		//constructor
		public Layout2d_constraint():base(){
		}
		public Layout2d_constraint (Vector2d size):this(){
			V_size = size;
		}
        public Layout2d_constraint(long x, long y):this(new Vector2d(x,y)){
        }

		private Vector2d V_size = new Vector2d();

	}
}

