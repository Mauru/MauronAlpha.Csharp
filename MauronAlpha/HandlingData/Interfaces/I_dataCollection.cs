

namespace MauronAlpha.HandlingData.Interfaces {
	
	public interface I_dataCollection<Type_key,Type_value> {

		Type_value Value( Type_key key );

		I_dataCollection<Type_key,Type_value> SetValue(Type_key key, Type_value value);

	}
}
