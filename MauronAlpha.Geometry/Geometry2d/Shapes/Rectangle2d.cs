using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A Rectangle
	public class Rectangle2d : Polygon2d {
		public Rectangle2d():base(){}
		public Rectangle2d ( Vector2d size ):this(0,0,size.X,size.Y) {}
		public Rectangle2d(Vector2d[] v):this(){
            if (v.Length != 4) {
                throw Error("Invalid Rectangle!,{" + v.Length + "},(Constructor)", this, ErrorType_constructor.Instance);
            }
			SetPoints(v);
		}
        public Rectangle2d (Vector2d position, Vector2d size):this(position.X,position.Y,size.X-position.X,size.Y-position.Y){}
		public Rectangle2d(double x, double y, double width, double height):base(){
			SetPoints(new Vector2d[4] {new Vector2d(x,y),new Vector2d(x+width,y),new Vector2d(x+width,y+height),new Vector2d(x,y+height)});
		}

		public new Rectangle2d SetIsReadOnly (bool state) {
			base.SetIsReadOnly(state);
			return this;
		}

        public new Rectangle2d Instance { get {
            int length = Points.Length;
            if (length == 0)
                return new Rectangle2d(0,0,0,0);
            if(length!=4)
                throw Error("Invalid Rectangle!,{"+length+"},(Instance)",this,ErrorType_bounds.Instance);
            return new Rectangle2d(Points[0], Points[2]);
        } }

	}
}
