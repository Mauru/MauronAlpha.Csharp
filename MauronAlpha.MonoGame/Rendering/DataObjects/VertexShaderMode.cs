namespace MauronAlpha.MonoGame.Rendering.DataObjects {
//Describes how a mesh is rendered;
public class VertexShaderMode : MonoGameComponent {
	string _name;
	public string Name {
		get { return _name; }
	}
	private VertexShaderMode(string name): base() {
		_name = name;
	}

	public static VertexShaderMode VertexPosition2d {
		get { return new VertexShaderMode("VertexPosition2d"); }
	}

	public static VertexShaderMode VertexPositionColor2d {
		get { return new VertexShaderMode("VertexPositionColor2d"); }
	}

	public bool Equals(VertexShaderMode other) {
		return _name.Equals(other.Name);
	}
}
}