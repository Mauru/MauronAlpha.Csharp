namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	using MauronAlpha.MonoGame.Geometry.DataObjects;

	public class LineBuffer:List<MonoGameLine> {
		public LineBuffer() : base() { }
		public LineBuffer(I_polygonShape2d shape) : this() {
			Segment2dList segments = shape.Segments;
			foreach (Segment2d s in segments)
				Add(new MonoGameLine(s));
			_bounds = shape.Bounds;
		}
		public LineBuffer(Segment2dList segments): base() {
			foreach (Segment2d s in segments)
				Add(new MonoGameLine(s));
		}
		public LineBuffer(Segment2dList segments, int thickness)	: base() {
			foreach (Segment2d s in segments)
				Add(new MonoGameLine(s,thickness));
		}


		public void Add(Segment2d segment) {
			MonoGameLine line = new MonoGameLine(segment);
			_bounds = null;
			base.Add(line);
		}
		public void Offset(Vector2d v) {
			foreach (MonoGameLine l in this)
				l.Offset(v);
			_bounds = null;
		}
		public void SetThickness(double n) {
			foreach (MonoGameLine l in this)
				l.SetThickness(n);
		}

		Polygon2dBounds _bounds;
		public Polygon2dBounds Bounds {
			get {
				if (_bounds == null)
					_bounds = GenerateBounds(this);
				return _bounds;
			}
		}
		public void SetBounds(Polygon2dBounds bounds) {
			_bounds = bounds;
		}
		public static Polygon2dBounds GenerateBounds(LineBuffer lines) {
			Vector2d min = null;
			Vector2d max = null;
			Vector2dList s;
			foreach (MonoGameLine line in lines) { 
				s = line.Segment.Points;
				foreach (Vector2d p in s) {
					if(min==null) {
						min = p;
						max = p;
					}
					else {
						if (min.X > p.X)
							min.SetX(p.X);
						if (min.Y > p.Y)
							min.SetY(p.Y);
						if (max.X < p.X)
							max.SetX(p.X);
						if (max.Y < p.Y)
							max.SetY(p.Y);
					}
				}
			}

			return Polygon2dBounds.FromMinMax(min, max);
		}

	}

}