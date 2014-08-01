using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Utility;

namespace MauronAlpha.Geometry.Geometry2d.Transformation {

	//Keeps track of all applied transforms
	public class Matrix2d : GeometryComponent2d {
		//constructor
		public Matrix2d(Shape2d shape) {}

		#region Rotation relative to center
		protected double INT_rotation=0;
		public double Rotation {
			get {	
				return INT_rotation;
			}
		}
		public Matrix2d SetRotation(double rotation){
			INT_rotation=rotation;
			return this;
		}
		#endregion
			
		#region Scale relative to center
		protected Vector2d V_scale=new Vector2d(1);
		public Vector2d Scale { get { return V_scale; } }
		public Matrix2d SetScale (Vector2d v) {
			V_scale=v;
			return this;
		}
		#endregion
		
		#region Shear relative to center
		protected Vector2d V_shear=new Vector2d(0);
		public Vector2d Shear { 
			get { return V_shear; }
		}
		public Matrix2d SetShear(Vector2d v){
			V_shear=v;
			return this;
		}
		#endregion

		#region Translation of the Center
		protected Vector2d V_translation=new Vector2d(0);
		public Vector2d Translation { get { return V_translation; } } 
		public Matrix2d SetTranslation(Vector2d v) {
			V_translation=v;
			return this;
		}
		#endregion

		#region The Center Point or identity point of the shape, we are going to leave this fixed for now
		public Vector2d Center=new Vector2d(0,0);
		#endregion

		//reset any modifications
		public Polygon2d RemoveFrom (Polygon2d pp) {
			Vector2d center=GeometryHelper2d.PolygonCenter(pp);
			Vector2d s=new Vector2d(1).Divide(Scale);

			foreach( Vector2d v in pp.Points ) {
				//Reset Rotation
				v.Rotate(center, Rotation*-1);

				//Scale
				v.Transform(center, s);

				//Offset
				v.Subtract(Translation);
			}
			return pp;
		}
		public Vector2d[] RemoveFrom (Vector2d[] pp) {
			Vector2d center=GeometryHelper2d.PolygonCenter(pp);
			Vector2d s=new Vector2d(1).Divide(Scale);

			foreach( Vector2d v in pp ) {
				//Reset Rotation
				v.Rotate(center, Rotation*-1);

				//Scale
				v.Transform(center, s);

				//Offset
				v.Subtract(Translation);
			}
			return pp;
		}

		//apply matrix
		public Polygon2d ApplyTo (Polygon2d pp) {
			Vector2d center=GeometryHelper2d.PolygonCenter(pp);
			foreach( Vector2d v in pp.Points ) {
				//Reset Rotation
				v.Rotate(center, Rotation);

				//Scale
				v.Transform(center, Scale);

				//Offset
				v.Add(Translation);
			}
			return pp;
		}
		public Vector2d[] ApplyTo (Vector2d[] pp) {
			Vector2d center=(Vector2d) GeometryHelper2d.PolygonCenter(pp).Divide(2);

			foreach( Vector2d v in pp ) {
				//Reset Rotation
				v.Rotate(center, Rotation);

				//Scale
				v.Transform(center, Scale);

				//Offset
				v.Add(Translation);
			}
			return pp;
		}


	}

}
