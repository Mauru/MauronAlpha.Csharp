using System;
using System.Collections.Generic;

namespace MauronAlpha.HandlingData {
	
	public class MauronCode_dataMapArray<T>:MauronCode_dataMap<MauronCode_dataList<T>>,IEnumerable<T> {
		public MauronCode_dataMapArray<T> SetValue(string key, T obj){
			if(!base.ContainsKey(key)){
				base.SetValue(key,new MauronCode_dataList<T>());
			}
			return this;
		}


		public IEnumerator<T> GetEnumerator ( ) {
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			throw new NotImplementedException();
		}
	}

}
