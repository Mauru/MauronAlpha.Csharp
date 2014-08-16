using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text {

	public class TextContext<PARENT,CHILD>:MauronCode_dataObject {

		private TextComponent_text TXT_source;
		public TextComponent_text Source {
			get {
				if(TXT_source==null){
					Error("Source can not be null",this);
				}
				return TXT_source;
			}
		}
		public TextContext<PARENT,CHILD> SetSource(TextComponent_text source) {
			TXT_source=source;
			return this;
		}

		private TextComponent_text TXT_parent;
		public TextComponent_text Parent {
			get {
				if( TXT_parent==null ) {
					Error("Source can not be null", this);
				}
				return TXT_parent;
			}
		}
		public TextContext<PARENT, CHILD> SetParent (TextComponent_text source) {
			TXT_source=source;
			return this;
		}

		private MauronCode_dataList<CHILD> DATA_children;
		public MauronCode_dataList<CHILD> Children {
			get {
				if(DATA_children==null){
					DATA_children=new MauronCode_dataList<CHILD>();
				}
				return DATA_children;
			}
		}
		public TextContext<PARENT, CHILD> AddChild(CHILD obj){
			AddChild(obj);
			return this;
		}

		//constructor
		public TextContext():base(DataType_maintaining.Instance) {}

	}

}