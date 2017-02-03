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

			//Clear the screen
			renderer.ClearScreen(renderer.StatusColor);

			PreRenderRequests requests = scene.PreRenderRequests;
			Stack<PreRenderChain> chains = null;
			if (requests.TryChains(ref chains)) {
				PreRenderChain chain=null;
				while (chains.TryPop(ref chain))
					HandlePreRenderChain(renderer, time, scene, chain);
			}
		
			//Get the Combiner-Stack
			Stack<CompositeBuffer> composites = null;
			if (requests.TryComposites(ref composites)) {
				CompositeBuffer buffer = null;
				while (composites.TryPop(ref buffer))
					GenerateSpriteBufferFromComposites(renderer, time, scene, buffer);
			}



		}
		public static void HandlePreRenderChain(GameRenderer renderer, long time, I_GameScene scene, PreRenderChain chain) {
			foreach (PreRenderProcess process in chain)
				PreRenderer.ExecutePreRenderProcess(renderer, process, time);
		}
		public static void GenerateSpriteBufferFromComposites(GameRenderer renderer, long time, I_GameScene scene, CompositeBuffer buffer) {
			SpriteBuffer result = new SpriteBuffer();
			foreach (RenderComposite composite in buffer) {

				PreRenderChain chain = composite.Chain;

				foreach (PreRenderProcess process in chain) {
					PreRenderedTexture texture = PreRenderedTexture.FromPreRenderProcess(renderer, process);

				}

			}
		}

		public static void FinalizeComposites(GameRenderer renderer, long time) { }

		public static void Print(long val) {
			Print("" + val);
		}
		public static void Print(string val) {
			System.Diagnostics.Debug.Print(val);
		}
	}


}

namespace MauronAlpha.MonoGame.Rendering.Events {

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;

	public class PreRenderChainCompleteHandler : MonoGameComponent, I_subscriber<PreRenderProcessComplete> {

		public PreRenderChainCompleteHandler(PreRenderChain chain):base() {

			_length = chain.Count;
		}

		int _length = 0;
		int _index = 0;

		public bool ReceiveEvent(PreRenderProcessComplete e) {
			_index++;
			return true;
		}

		public bool Equals(I_subscriber<PreRenderProcessComplete> other) {
			return Id.Equals(other.Id);
		}
	}

}