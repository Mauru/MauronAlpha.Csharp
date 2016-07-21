using MauronAlpha.MonoGame.Interfaces;

namespace MauronAlpha.MonoGame.Geometry {
	
	//Describes properties of a shape
	public abstract class ShapeDefinition:MonoGameComponent {

		public abstract string Name { get; }
		public abstract bool UsesSpriteBatch { get; }
		public abstract bool UsesVertices { get; }
		public abstract bool IsPolygon { get; }

		public bool Equals(ShapeDefinition other) {
			return Name.Equals(other.Name);
		}
	
	}

}
