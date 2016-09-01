namespace MauronAlpha.MonoGame.Rendering {
	using MauronAlpha.Events.Units;

	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	public class RenderEvent:EventUnit_event {

		RenderRequest _request;
		public RenderRequest Request { get { return _request; } }
		
		public I_RenderResult Result { get { return _request.Result; } }

		public long Time {
			get {
				if(_request == null)
					return 0;
				return _request.Time;
			}
		}

		public RenderEvent(RenderRequest request):base("Rendered") {
			_request = request;		
		}
	}
}
