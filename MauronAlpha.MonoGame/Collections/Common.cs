using MauronAlpha.HandlingData;

namespace MauronAlpha.MonoGame.Collections {

	public class List<T> :MauronCode_dataList<T> {} //ordered collection >> object
	public class Registry<T> :MauronCode_dataMap<T> {
		public bool TryIndex(long index, ref T result) {
			if (base.Values.Count < 1)
				return false;
			long count = -1;
			foreach(T candidate in base.Values) {
				count++;
				if(count == index) {
					result = candidate;
					return true;
				}
			}
			return false;
		}
	
	} //text >> object
	public class Index<T> :MauronCode_dataIndex<T> { } //unordered collection >> object
	
	/// <summary>	KeyValue Pair - key : object	</summary>
	public class Set<T,V> :MauronCode_dataRelation<T,V> {
		public Set() :base() { }
		public Set(T key, V value)	: base(key,value) {	}
	}
	public class Stack<T> :MauronCode_dataStack<T> { } //Noncounting collection >> object

	public class Assign<K, V> : MauronCode_dataReference<K, V> {

		public List<Set<K,V>> AsKVPList() {
			List<Set<K, V>> result = new List<Set<K, V>>();
			if (IsEmpty)
				return result;

			foreach (K k in Keys) {
				Set<K, V> kvp;
				V v = default(V);
				if (TryFind(k, out v))
					kvp = new Set<K,V>(k,v);

				else {
					kvp = new Set<K, V>();
					kvp.SetKey(k);
					kvp.UnsetValue();
				}
				result.Add(kvp);
			}
			return result;

		}

	}

}

namespace MauronAlpha.MonoGame.DataObjects {

	public class FileResource:MonoGameComponent { }

}