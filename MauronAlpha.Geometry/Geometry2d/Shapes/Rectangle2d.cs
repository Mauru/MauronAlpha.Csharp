using MauronAlpha.HandlingErrors;

using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Interfaces;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {


	public class Rectangle2d : Polygon2d, I_polygon2d<Rectangle2d> {

        //constructors
		public Rectangle2d():base(){}
        
        //creators
		private Rectangle2d ( Vector2d size ):this(0 ,0 ,size.X ,size.Y) {}
        
		private Rectangle2d(Vector2dList points):this(){
            if (points.Count != 4) {
				throw Error( "Invalid Rectangle!,{" + points.Count + "},(Constructor)", this, ErrorType_constructor.Instance );
            }

			//order the points
			points = points.Ordered_asLRTB;

			SetPoints(points);


		}

        private Rectangle2d (Vector2d position, Vector2d size):this(
            position.X,
            position.Y,
            size.X - position.X,
            size.Y - position.Y
        ){}

		public Rectangle2d(double x, double y, double width, double height):base(){
            Vector2dList points = new Vector2dList().AddValues( new Vector2d[4] {
               new Vector2d(x, y),
               new Vector2d(x + width, y),
               new Vector2d(x + width, y + height),
               new Vector2d(x, y + height)
            }, false);
			SetPoints(points);
		}

		public static Rectangle2d FromBounds( Polygon2dBounds bounds ) {
			Rectangle2d result = new Rectangle2d( bounds.Points );
			return result;
		}

		public new Rectangle2d SetIsReadOnly (bool state) {
			base.SetIsReadOnly(state);
			return this;
		}
        public new Rectangle2d Instance { get {
            int length = Points.Count;
            if (length == 0)
                return new Rectangle2d(0,0,0,0);
            if(length!=4)
                throw Error("Invalid Rectangle!,{"+length+"},(Instance)",this,ErrorType_bounds.Instance);
            return new Rectangle2d(Points.Value(0), Points.Value(2));
        } }

	}

}
