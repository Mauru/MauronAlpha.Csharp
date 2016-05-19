using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities;
using MauronAlpha.MonoGame.WorldGenerator.Units;

namespace MauronAlpha.MonoGame.WorldGenerator.DataObjects {
	
	public class WorldBluePrint:EntityComponent {

		public Geography CreateGeography(Site site, World world) {

			return new Geography();

		}

		public Memories CreateBasicHistory(Site site, World world) {

			return new Memories();

		}

	}
}
