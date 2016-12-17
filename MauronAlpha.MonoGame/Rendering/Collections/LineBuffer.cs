namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	using MauronAlpha.MonoGame.Geometry.DataObjects;

	public class LineBuffer:List<MonoGameLine> {
		public LineBuffer() : base() { }
		public LineBuffer(I_polygonShape2d shape) : this(shape.Segments) { }
		public LineBuffer(Segment2dList segments): base() {
			foreach (Segment2d s in segments)
				Add(new MonoGameLine(s));
		}
		public void Add(Segment2d segment) {
			MonoGameLine line = new MonoGameLine(segment);
			base.Add(line);
		}
		public void Offset(Vector2d v) {
			foreach (MonoGameLine l in this)
				l.Offset(v);
		}
		public void SetThickness(double n) {
			foreach (MonoGameLine l in this)
				l.SetThickness(n);
		}
	}

}