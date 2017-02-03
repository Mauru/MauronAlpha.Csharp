namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Events;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.Events.Interfaces;

	public class Scene_BasicAlpha : GameScene, I_subscriber<CompositeRenderRequestComplete> {

		public Scene_BasicAlpha(GameManager game) : base(game) { }

		public override GameRenderer.DrawMethod DrawMethod {
			get { return CompositeRenderer.DrawMethod; }
		}

		public override void Initialize() {
			GameRenderer renderer = Game.Renderer;

			//Set up shader
			DefaultShader shader = DefaultShader.Create2DTopLeft(Game);
			renderer.SetCurrentShader(shader);

			FormObjectBuffer();

			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(CompositeRenderer.DrawMethod);

			base.Initialize();
		}

		public void FormObjectBuffer() {
			RenderComposite composite = new RenderComposite(Game);
			//Create RenderChain
			PreRenderChain chain = new PreRenderChain(Game) {
				PreRenderProcess.FromShape(new Hexagon2d())
			};

			CompositeBuffer buffer = new CompositeBuffer() { composite };
			PreRenderRequests.Add(chain);
			PreRenderRequests.Add(buffer);
		}

		public override void ReceiveStatus(Rendering.Interfaces.I_RenderStatus status) {
			System.Diagnostics.Debug.Print(status.Message);
		}

		public bool ReceiveEvent(CompositeRenderRequestComplete e) {
			e.Target.UnSubscribe(this);
			Game.Renderer.SetDrawMethod(CompositeRenderer.FinalizeComposites);
			return true;
		}
		public bool Equals(I_subscriber<CompositeRenderRequestComplete> other) {
			return Id.Equals(other.Id);
		}

		public static void Print(long val) {
			Print("" + val);
		}
		public static void Print(string val) {
			System.Diagnostics.Debug.Print(val);
		}

	}
}
