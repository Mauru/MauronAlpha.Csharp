using System;

using MauronAlpha.HandlingData;

using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;
using MauronAlpha.Text.Collections;


namespace MauronAlpha.Text.Interfaces {
	
	//General
	public interface I_textUnit:IEquatable<I_textUnit>, I_protectable {

		string AsString { get; }

		TextUnitType UnitType { get; }

		I_textEncoding Encoding { get; }

		I_textUnit Parent { get; }
		I_textUnit<I_textUnit<TextUnit_text>> Source { get; }

		bool IsFirstChild { get; }
		bool IsLastChild { get; }
		bool IsEmpty { get; }
		bool IsReadOnly { get; }
		
		bool HasMultiWord { get; }
		bool HasWhiteSpace { get; }
		bool HasLineBreak { get; }
		bool HasLimit { get; }
		bool HasReachedLimit { get; }

		int Index { get; }
		int Limit { get; }
		int ChildCount { get; }

		TextContext Context { get; }

		I_textUnit FirstChild { get; }
		I_textUnit LastChild { get; }
		I_textUnit NewChild { get; }
		I_textUnit ChildByIndex (int index);

		MauronCode_dataList<I_textUnit> Children { get; }

		I_textUnit<TextUnitType_character> FirstCharacter { get; }
		I_textUnit<TextUnitType_character> LastCharacter { get; }

	}

	//Generic
	public interface I_textUnit<T> : I_textUnit,
	IEquatable<T> where T : I_textUnit {
	
		T SetIsReadOnly (bool b_isReadOnly);

		T SetEncoding (I_textEncoding encoding);

		T Instance { get; }
		
		MauronCode_dataIndex<I_textUnit<T>> Neighbors { get; }

	}

}
