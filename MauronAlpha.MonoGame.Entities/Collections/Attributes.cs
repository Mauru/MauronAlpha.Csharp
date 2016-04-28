using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {

	public class Attributes:EntityComponent {

		MauronCode_dataList<Attribute> Members = new MauronCode_dataList<Attribute>();

		public void Add(string name) {
			Attribute newAttribute = new Attribute(name);
			Members.Add(newAttribute);
	
		}
		public bool Contains(Attribute unit) {
			foreach (Attribute candidate in Members) {
				if (candidate.Name == unit.Name)
					return true;
			}
			return false;
		}
		public Attribute ByName(string name) {
			foreach (Attribute candidate in Members) {
				if (candidate.Name == name)
					return candidate;
			}
			return Attribute.Default;			
		}

		public MauronCode_dataMap<Modifiers> Modifiers = new MauronCode_dataMap<Modifiers>();
		public void AddModifier(string name, Modifier<EntityValueType,EntityValueType> modifier, EntityValue<T_Time> time)  {
			if (!Modifiers.ContainsKey(name)) {
				Modifiers those = new Modifiers();
				Modifiers.SetValue(name, those);
			}
			Modifiers these = Modifiers.Value(name);
			these.Add(modifier);
		}
	}
}
