using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Memories:EntityComponent {

		MauronCode_dataList<Memory> Members;

		public void Add(Memory memory) {
			if (Members == null)
				Members = new MauronCode_dataList<Memory>();
			Members.Add(memory);
		}

		public static Memories Empty {
			get {
				return new Memories();
			}
		}
	
	}
}
