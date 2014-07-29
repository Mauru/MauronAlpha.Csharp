using MauronAlpha.GameEngine.Positioning;
using MauronAlpha.GameEngine.Events;
using MauronAlpha.GameEngine.Rendering.Textures;
using MauronAlpha.GameEngine.ObjectRelations;
using MauronAlpha.Geometry;

namespace MauronAlpha.GameEngine.Rendering {

	public interface I_Drawable : I_Positionable, I_RenderObjectParent, I_RenderObjectChild, I_GameEventListener,I_GameEventSender, I_GameComponent {
		
		string Name { get; }
		DrawableType DrawableType {get;}

		bool Visible {get;}
		bool HasRendered {get;}
		bool NeedsRendering {get;}
		bool RenderSheduled {get;}
		void SheduleRender ( );

		GameEventShedule RenderShedule { get; }
		void GenerateRenderData ( );
		GameRenderData RenderData {get;set;}

	}

}