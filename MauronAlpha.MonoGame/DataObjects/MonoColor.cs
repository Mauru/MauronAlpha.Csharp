namespace MauronAlpha.MonoGame.DataObjects {
	
	public class MonoColor:MonoGameComponent {

		int _r;
		public int R { get { return _r; } }
		int _g;
		public int G { get { return _g; } }
		int _b;
		public int B { get { return _b; } }

		int _a;
		public int A { get { return _a; } }

		public MonoColor(int r, int g, int b, int alpha):base() {
			_r = r; _g = g; _b = b; _a = alpha;
		}

		uint _packed;
		public uint AsUint {
			get { return _packed; }
		}
	}
}
