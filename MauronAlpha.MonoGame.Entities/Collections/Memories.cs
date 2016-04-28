using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Memories:EntityComponent {

		MauronCode_dataList<Memory> Members = new MauronCode_dataList<Memory>();

		public void Add(Memory memory) {
			Members.Add(memory);
		}


	}
}
