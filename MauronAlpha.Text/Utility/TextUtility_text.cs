using MauronAlpha.Text.Units;

using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Utility {
	
	//A utility for comparison in TextComponent_unit
	public class TextUtility_text:TextUtility {

		public bool RangeCompare<T>(MauronCode_dataList<T> units, int start, int end, MauronCode_dataList<char> symbols) where T:TextComponent_unit {
			
			return false;
		}

		public MauronCode_dataList<TextComponent_unit> INTERFACE_CovertToUnitList<T>(MauronCode_dataList<T> units) where T:TextComponent_unit {
			MauronCode_dataList<TextComponent_unit> result = new MauronCode_dataList<TextComponent_unit>();
			foreach(T unit in units){
				result.AddValue(unit);
			}
			return result;
		}
		public MauronCode_dataIndex<TextComponent_unit> INTERFACE_CovertToUnitIndex<T> (MauronCode_dataIndex<T> units) where T : TextComponent_unit {
			MauronCode_dataIndex<TextComponent_unit> result=new MauronCode_dataIndex<TextComponent_unit>();
			foreach(int n in units.Keys){
				result.SetValue(n,units.Value(n));
			}
			return result;
		}
	
		public TextComponent_unit INTERFACE_CharToUnit(TextUnit_character source, char c) {
			return source.Instance;
		}
		public MauronCode_dataList<TextComponent_unit> INTERFACE_CharListToUnits (TextUnit_character source, MauronCode_dataList<char> units) {
			MauronCode_dataList<TextComponent_unit> result=new MauronCode_dataList<TextComponent_unit>();
			foreach( char unit in units ) {
				result.AddValue(INTERFACE_CharToUnit(source,unit));
			}
			return result;
		}
	}

}
