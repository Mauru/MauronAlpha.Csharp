

namespace MauronAlpha.MonoGame.SpaceGame {
	using MauronAlpha.ExplainingCode;
	using MauronAlpha.HandlingData;
	using MauronAlpha.HandlingErrors;

    public class GameComponent:MauronCode_component {}

	public class GameList<T> : MauronCode_dataList<T> { }

	public class GameEvent<T> : GameComponent where T:GameEventType { }

	public class GameEventType : GameComponent { }

	public class Assign<K, V> : MauronCode_dataReference<K, V> {

		public GameList<KVP<K,V>> AsKVPList() {
			GameList<KVP<K, V>> result = new GameList<KVP<K, V>>();
			if (IsEmpty)
				return result;

			foreach (K k in Keys) {
				KVP<K, V> kvp;
				V v = default(V);
				if (TryFind(k, out v))
					kvp = new KVP<K,V>(k,v);

				else {
					kvp = new KVP<K, V>();
					kvp.SetKey(k);
					kvp.UnsetValue();
				}
				result.Add(kvp);
			}
			return result;

		}

	}

	public class KVP<K,V>:MauronCode_dataRelation<K,V> {
		public KVP() : base() { }
		public KVP(K key, V value) : base(key, value) { }
	}

	public class CriticalGameError : MauronCode_error {
		public CriticalGameError(string message, MauronCode_component obj, string namespaceFunction) : base(message, obj, ErrorType_fatal.Instance) { }
	}
}
