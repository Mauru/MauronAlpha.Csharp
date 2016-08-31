namespace MauronAlpha.MonoGame.Assets {
	using MauronAlpha.Events.Units;

	public class AssetLoadEvent :EventUnit_event {

		public AssetLoadEvent(AssetGroup group):base("Loaded") {
			_group = group;
		}
		AssetGroup _group;
		public AssetGroup Target {
			get { return _group; }
		}

	}


}
