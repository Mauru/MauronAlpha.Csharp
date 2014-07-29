using System.Collections.Generic;
using System;

using MauronAlpha.GameEngine.Rendering;
using MauronAlpha.GameEngine.ObjectRelations;
using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Text {

	//A piece of text
	public class TextComponent_text : Drawable_container, I_TextComponent {
		public Rectangle2d MaxSize;

		//the text
		private string STR_text="";
		public string Text { 
			get { 
				return STR_text;
			}		
		}
		public TextComponent_text SetText (string text) {
			STR_text=text;
			return this;
		}

		//the font to use as renderobject
		private I_Font FNT_font;
		public I_Font Font { get{
			return FNT_font;
		} }
		public TextComponent_text SetFont(I_Font font){
			FNT_font=font;
			return this;
		}

		//the number of lines in the text
		private int INT_numlines=0;
		public int NumLines { get { return INT_numlines; } }

		//the number of words in the text
		private int INT_numwords=0;
		public int NumWords { get { return INT_numwords; } }

		//constructor
		public TextComponent_text (I_Drawable parent, Stack<Stack<char>> words, Rectangle2d maxsize, string name)
			: base(parent,name) {
			MaxSize=maxsize;
			Vector2d offset=new Vector2d();
			double max_x=0;
			while( words.Count>0 ) {
				NumWords++;
				Stack<char> word=words.Pop();
				TextComponent_word t_word=new TextComponent_word(this,word);
				t_word.SetPosition(offset.Instance);
				if( t_word.HasLinebreak ) {
					offset.Add(new Vector2d(t_word.Width, t_word.Height));
					if( offset.X>max_x ) { max_x=offset.X; }
					offset.X=0;
					NumLines++;
				}
				else {
					offset.Add(new Vector2d(t_word.Width, 0));
				}
				AddChild(t_word);
			}
			if( Children.Length>0 ) {
				NumLines++;
				offset.Y+= LastChild.Bounds.Height;
			}
			if( NumLines==1 ) {
				max_x=offset.X;
			}
			//Height=offset.Y;
			//Width=max_x;
		}

		#region I_TextComponent
			//The Font Asset
			public GameAsset Asset {
				get {
					return Source.Font.Asset;
				}
			}

			//inherited from Textcomponent
			public string AsString {
				get {
					string r="";
					foreach( TextComponent_word c in Children ) {
						r+=c.AsString;
					}
					return r;
				}
			}

			//Get the Source TextDisplay
			private TextDisplay D_source;
			public TextDisplay Source {
				get {
					if( D_source==null ) {
						D_source=(TextDisplay) Parent;
					}
					return D_source;
				}
				set {
					if( D_source==null ) {
						D_source=(TextDisplay) value;
					}
				}
			}

		#endregion

		#region Drawable
			public override void GenerateRenderData ( ) {
				throw new System.NotImplementedException();
			}
		#endregion
	}


}
