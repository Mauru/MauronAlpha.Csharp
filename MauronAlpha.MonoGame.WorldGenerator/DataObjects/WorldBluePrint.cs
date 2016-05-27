using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities;
using MauronAlpha.MonoGame.WorldGenerator.Units;

namespace MauronAlpha.MonoGame.WorldGenerator.DataObjects {
	
	public abstract class WorldBluePrint:EntityComponent {

		public abstract Geography CreateGeography(Site site, World world);

		public abstract Memories CreateBasicHistory(Site site, World world);

		public abstract Population CreatePopulation(Site site, World world);

	}
}
