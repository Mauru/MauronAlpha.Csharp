using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Position {

	//Determines limits on size
	public class Layout2d_constraint:Layout2d_component, I_protectable<Layout2d_constraint> {

		//constructor
		public Layout2d_constraint():base(){
		}
		public Layout2d_constraint (Vector2d size):this(){
			V_size = size;
		}
        public Layout2d_constraint(long x, long y):this(new Vector2d(x,y)){
        }

		private Vector2d V_size = new Vector2d();
		public Vector2d AsVector2d { get { return V_size.Instance; } }

		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
		
		public Layout2d_constraint SetIsReadOnly(bool status) {
			B_isReadOnly = status;
			return this;
		}
		public Layout2d_constraint Instance {
			get {
				return new Layout2d_constraint(V_size.Instance);
			}
		}

	}
}

