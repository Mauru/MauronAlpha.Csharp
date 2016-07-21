namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.Logic.DataObjects;

	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Utility;

	public class T_Percent : ValueType {

		public override GameName Name {
			get { return new GameName("Percent"); }
		}

	}
	public class T_String : ValueType {
		public override GameName Name {
			get { return new GameName("String"); }
		}

	}
	public class T_Int : ValueType {
		public override GameName Name {
			get { return new GameName("Int"); }
		}

		public string NullAsString {
			get {
				return "NULL";
			}
		}
		public int NullAsInt {
			get {
				return 0;
			}
		}

		public bool CanBeNull {
			get {
				return true;
			}
		}
		public GameValue<T_Int> Default {
			get {
				return new GameValue<T_Int>(true);
			}
		}

	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.Logic.DataObjects;
	using MauronAlpha.MonoGame.Collections;

	public class WeightedList<K, V> : Assign<GameValue<K>, List<V>> where K:ValueType,new() {

		public WeightedList<K, V> Set(GameValue<K> key, V value) {
			GameList<V> values = new GameList<V>();
			if (!base.TryFind(key, out values)) { 
				values.Add(value); 
				base.SetValue(key, values);
			}
			return this;
		}

	}

}
