using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.HandlingData {

	public class MauronCode_dataList<T>:MauronCode_dataObject {
		public List<T> Data;
		public MauronCode_dataList(Type T) {
			
		}
		public T[] AsArray { get {
			return Data.ToArray();
		} }
	}

}
