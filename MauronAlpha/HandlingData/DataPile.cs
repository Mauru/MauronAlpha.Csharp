using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.HandlingData {
	
	//Named Piles of Objects T (Pile = list)
	public class DataPile<T>:MauronCode_dataMap<MauronCode_dataList<T>>,IEnumerable<T> {

		public void Add(string key, T value) {
			MauronCode_dataList<T> map = new MauronCode_dataList<T>();
			bool result = this.TryGet(key, ref map);
			if (!result)
				return;
			map.Add(value);
		}
		public MauronCode_dataList<T> Get(string key) {
			MauronCode_dataList<T> map = new MauronCode_dataList<T>();
			bool result = this.TryGet(key, ref map);
			return map;
		}

		public MauronCode_dataList<T> Combined(string[] keys) {
				MauronCode_dataList<T> result = new MauronCode_dataList<T>();
				MauronCode_dataList<T> map = new MauronCode_dataList<T>();
				foreach (string key in keys) {
					bool found = TryGet(key, ref map);
					if(found)
						result.AddValuesFrom(map);
				}
				return result;
		}


		public IEnumerator<T> GetEnumerator() {
			return this.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
	}

}
