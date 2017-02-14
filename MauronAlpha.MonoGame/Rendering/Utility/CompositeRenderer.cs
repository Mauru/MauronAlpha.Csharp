namespace MauronAlpha.MonoGame.Rendering.Utility {

	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Events;

	/// <summary> Utility class for combining RenderCompostes </summary>
	public class CompositeRenderer:MonoGameComponent {

		public static GameRenderer.DrawMethod DrawMethod {
			get {
				return DrawCycle;
			}
		}

		public static void SolveRenderChain(GameRenderer renderer, long time) { }

		/// <summary> Cycle method for handling renderComposites </summary>
		public static void DrawCycle(GameRenderer renderer, long time) {

			I_GameScene scene = renderer.CurrentScene;



		}


		public static void Print(long val) {
			Print("" + val);
		}
		public static void Print(string val) {
			System.Diagnostics.Debug.Print(val);
		}
	}


}