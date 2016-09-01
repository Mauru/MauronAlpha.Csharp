using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.MonoGame.Resources {

	public class GameTexture:GameResource {

		public GameTexture() : base(ResourceType_gameTexture.Instance) { }
	}

	public class ResourceType_gameTexture : ResourceType {
		public override String Name {
			get { return "GameTexture"; }
		}
		public static ResourceType_gameTexture Instance {
			get { return new ResourceType_gameTexture(); }
		}
	}

}
