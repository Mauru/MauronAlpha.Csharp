namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Geometry.DataObjects;

	public class LineBuffer:List<MonoGameLine> {


		public LineBuffer() : base() { }
		public LineBuffer(Segment2dList segments): base() {
			System.Diagnostics.Debug.Print("" + segments.Count);
			foreach (Segment2d s in segments) { 

				Add(new MonoGameLine(s));
			}

		}
	}

}