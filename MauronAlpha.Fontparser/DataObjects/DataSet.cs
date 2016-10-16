namespace MauronAlpha.FontParser.DataObjects {
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.HandlingData;

	/// <summary> A parsed set of data, auto-empties on read </summary>
	public class DataSet : FontParserComponent {
		MauronCode_dataStack<Word> Data;

		public Word Key;
		public bool HasKey {
			get { return Key != null; }
		}
		public bool HasValue {
			get {
				return !Data.IsEmpty;
			}
		}
		public bool IsEmpty {
			get { return Key == null; }
		}

		public bool IsSetOfData = false;

		public DataSet() : base() { }

		public long Count {
			get {
				if (Data == null)
					return 0;
				return Data.Count;
			}
		}

		/// <summary> Append word to value </summary>
		public void Append(Word w) {
			if (Data == null) {
				Data = new MauronCode_dataStack<Word>();
			}
			Data.Add(w);

		}
		/// <summary> Append a character to the key </summary>
		public void AppendToKey(Character c) {
			if (Key == null)
				Key = new Word();
			Key.Add(c);
		}

		/// <summary> Return value without emptying dataset </summary>
		public string PreviewValue() {
			string result = "";
			MauronCode_dataStack<Word> p = new MauronCode_dataStack<Word>();
			if (Data == null) {
				return "EMPTY";
			}
			while (!Data.IsEmpty) {
				Word w = Data.Pop;
				result += w.AsVisualString;
				p.Add(w);
			}
			Data = p;
			return result;
		}

		/// <summary> Read value as string, empties object </summary>
		public string ValueAsString {
			get {
				if (Data == null)
					return "";
				string result = "";
				while (!Data.IsEmpty) {
					Word w = Data.Pop;
					result = w.AsString + result;
				}
				return result;
			}
		}
		public int ValueAsInt {
			get {
				if (IsEmpty || IsSetOfData)
					return 0;

				Word w = Data.Pop;
				return WordAsInt32(w);
			}
		}

		public int WordAsInt32(Word w) {
			return System.Int32.Parse(w.AsString);
		}
		public bool ValueAsBool {
			get {
				if (IsEmpty || IsSetOfData)
					return false;

				Word w = Data.Pop;
				return WordAsInt32(w) > 0;
			}
		}
		public Distance ValueAsDistance {
			get {
				if (!IsSetOfData)
					return new Distance();
				Distance result = new Distance();
				int ctr = 0;
				while (!Data.IsEmpty) {
					Word w = Data.Pop;
					if (ctr == 0)
						result.Top = WordAsInt32(w);
					else if (ctr == 1)
						result.Right = WordAsInt32(w);
					else if (ctr == 2)
						result.Bottom = WordAsInt32(w);
					else if (ctr == 3)
						result.Left = WordAsInt32(w);

					ctr++;
				}
				return result;
			}
		}
		public Distance ValueAsVector {
			get {
				if (!IsSetOfData)
					return new Distance();
				int ctr = 0;
				Distance result = new Distance();
				while (!Data.IsEmpty) {
					Word w = Data.Pop;
					int n = 0;
					if (ctr == 0) {
						n = WordAsInt32(w);
						result.Left = n;
						result.Right = n;
					}
					else if (ctr == 1) {
						n = WordAsInt32(w);
						result.Bottom = n;
						result.Bottom = n;
					}
					ctr++;
				}
				return result;
			}
		}

		public string DebugData() {
			string key = (Key == null) ? "NOKEY" : Key.AsString;
			string val = "";

			if (Data == null) {

				val = "NODATA";

			}
			else {

				val = "VALUES(" + Data.Count + ")";

			}

			return "{" + key + ":" + val + "}";
		}
	}

}