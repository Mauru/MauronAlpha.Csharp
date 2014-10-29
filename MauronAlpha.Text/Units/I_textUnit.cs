using MauronAlpha.HandlingData;

using MauronAlpha.Text.Context;
using MauronAlpha.Text.Collections;

using System;


namespace MauronAlpha.Text.Units {

	//Interface
	public interface I_textUnit<T>:IEquatable<T> where T:TextComponent_unit {

		string AsString {get;}
		TextUnitType UnitType { get; }

		bool IsEmpty {get;}
		bool IsReadOnly { get; }
		T SetIsReadOnly (bool b_isReadOnly);

		bool HasMultiWord { get; }
		bool HasWhiteSpace {get;}
		bool HasLineBreak {get;}
		bool HasLimit {get;}
		bool HasReachedLimit {get;}

		bool IsFirstChild {get;}
		bool IsLastChild {get;}
		
		TextContext Context { get; }

		int Index { get; }
		int Limit { get; }
		int ChildCount { get; }

		I_textUnit<T> Instance { get; }
		TextComponent_unit Parent { get; }
		I_textUnit<TextUnit_text> Source { get; }

		MauronCode_dataList<TextComponent_unit> Children { get; }
		TextComponent_unit FirstChild { get; }
		TextComponent_unit LastChild { get; }
		TextComponent_unit NewChild { get; }
		TextComponent_unit ChildByIndex (int index);

		MauronCode_dataIndex<TextComponent_unit> Neighbors { get; }

		TextUnit_character FirstCharacter { get; }
		TextUnit_character LastCharacter { get; }

	}
}
