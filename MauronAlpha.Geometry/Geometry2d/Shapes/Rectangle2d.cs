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
		private Rectangle2d(Vector2d start, double width, double height)	: this(start.X,start.Y,width,height) {}
		private Rectangle2d(double x, double y, double width, double height): base() {
			Vector2dList points = new Vector2dList() {
               new Vector2d(x, y),
               new Vector2d(x + width, y),
               new Vector2d(x + width, y + height),
               new Vector2d(x, y + height)
			};
			SetPoints(points);
		}
		private Rectangle2d(Vector2dList points):this(){
			SetPoints(points);
		}
        private Rectangle2d (Vector2d start, Vector2d size):this(start.X, start.Y, start.X + size.X, start.Y + size.Y){}
		private Rectangle2d(Vector2d a, Vector2d b, Vector2d c, Vector2d d) {
			SetPoints(new Vector2dList() {a,b,c,d});
		}

		public static Rectangle2d FromBounds( Polygon2dBounds bounds ) {
			Rectangle2d result = new Rectangle2d( bounds.Points );
			return result;
		}
		public static Rectangle2d CreateAlignCenter(double width, double height) {
			double x = width/2;
			double y = height/2;
			return new Rectangle2d(
				new Vector2d(-x, -y),
				new Vector2d(x, -y),
				new Vector2d(x, y),
				new Vector2d(-x, y)
			);
		}

		public static Rectangle2d CreateAlignTopLeft(double width, double height) {
			return new Rectangle2d(0, 0, width, height);
		}

		I_polygonShape2d I_polygonShape2d.Copy {
			get { return Copy; }
		}
		
	}

}
