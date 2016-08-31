namespace MauronAlpha.MonoGame.Collections {
	using MauronAlpha;
	using MauronAlpha.HandlingData;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Geometry;

	public class RenderInstructions :List<RenderInstruction> {
		public static RenderInstructions Default {
			get {
				return Rectangle;
			}
		}
		public static RenderInstructions Texture {
			get {
				return new RenderInstructions() {
					Render_Texture.Instance
				};
			}
		}
		public static RenderInstructions Text {
			get {
				return new RenderInstructions() {
					Render_Text.Instance
				};
			}
		}
		public static RenderInstructions Shape {
			get {
				return new RenderInstructions() {
					Render_Shape.Instance
				};
			}
		}
		public static RenderInstructions Lines {
			get {
				return new RenderInstructions() { 
					Render_Lines.Instance
				}; 
			}
		}
		public static RenderInstructions Rectangle {
			get { 
				return new RenderInstructions() { 
					Render_Rectangle.Instance
				};
			}
		}
		public static RenderInstructions Composite { 
			get {
				return new RenderInstructions() { 
					Render_Composite.Instance 
				};
			} 
		}
	}

	public sealed class Render_Composite :RenderInstruction {
		public override string Name {
			get { return "Composite"; }
		}

		static object _sync= new System.Object();
		static volatile Render_Composite _instance;

		Render_Composite() : base() { }
	
		public static Render_Composite Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new Render_Composite();
					}
				}

				return _instance;
			}
		}
	}
}
