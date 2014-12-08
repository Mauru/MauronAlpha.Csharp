using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace MauronAlpha.Geometry.Geometry2d.Collections {
    public class Polygon2dBounds : GeometryComponent2d {

        private Polygon2dBounds():base(){}
        public Polygon2dBounds(Polygon2d polygon) : this() {
        
            Vector2d min=null;
			Vector2d max=null;
			foreach( Vector2d v in polygon.Points ) {
				if( min==null ) {
					min=new Vector2d(v);
					max=new Vector2d(v);
				}
				else {
					if( v.X<min.X )
						min.SetX(v.X);
					if( v.Y<min.Y )
						min.SetY(v.Y);
					if( v.X>max.X )
						max.SetX(v.X);
					if( v.Y>max.Y )
						max.SetY(v.Y);
				}
			}
            V_min = min;
            V_max = max;
        }

        private Vector2d V_min;
        private Vector2d V_max;
        private Vector2d V_center;

        public Vector2d TopLeft {
            get {
                return V_min;
            }
        }
        public Vector2d BottomRight {
            get {
                return V_max;
            }
        }

        public Vector2d Center {
            get
            {
                if (V_center == null)
                {
                    V_center = TopLeft.Instance.Add(TopLeft.Difference(BottomRight).Divide(2));
                }
                return V_center;
            }
        }
    
    }
}
