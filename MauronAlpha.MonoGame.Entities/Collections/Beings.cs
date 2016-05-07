using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	
	public class Beings {
		MauronCode_dataList<Being> Members = new MauronCode_dataList<Being>();

		public void Add(Being being) {
			Members.Add(being);
		}
	
	}
}
