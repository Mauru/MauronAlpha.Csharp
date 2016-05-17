using MauronAlpha.MonoGame.Entities;
using MauronAlpha.HandlingData;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class SGroup<T> where T:EntityComponent {

		MauronCode_dataList<T> Data = new MauronCode_dataList<T>();

		public SGroup():base() {}

		public void Add(T item) {
			Data.Add(item);
		}

	}
}
