using System.Collections.Generic;
using System;

using MauronAlpha.GameEngine.Rendering;
using MauronAlpha.GameEngine.ObjectRelations;
using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Text {

	//A word in a text
	public class TextComponent_word : Drawable_container, I_TextComponent {

		public bool HasLinebreak=false;

		public TextComponent_word (TextComponent_text parent, Stack<char> word)
			: base(parent,word.ToString()) {
			//base offset
			Vector2d offset=new Vector2d();

			//cycle through the characters of a word
			while( word.Count>0 ) {
				char c=word.Pop();
				Vector2d v=Source.Font.CharacterSize(c);
				TextComponent_char ch=new TextComponent_char(this, c);
				if( TextDisplay.LinebreakCharacters.Contains(c) ) {
					HasLinebreak=true;
				}
				ch.SetPosition(offset.Instance);
				AddChild(ch);
				offset.Add(new Vector2d(v.X, 0));
			}

			Height=Source.Font.CharacterHeight;
			Width=offset.X;
		}

		public double Height {
			get { return Bounds.Height; }
			set { throw new NotImplementedException(); }
		}
		public double Width {
			get { return Bounds.Width; }
			set { throw new NotImplementedException(); }
		}

		public override void GenerateRenderData ( ) {
			throw new System.NotImplementedException();
		}

		public override Rectangle2d Bounds {
			get { throw new System.NotImplementedException(); }
		}

		public TextDisplay Source {
			get { throw new System.NotImplementedException(); }
		}

		public string AsString {
			get { throw new System.NotImplementedException(); }
		}

		TextDisplay I_TextComponent.Source {
			get {
				throw new System.NotImplementedException();
			}
			set {
				throw new System.NotImplementedException();
			}
		}



		public GameAsset Asset {
			get { throw new NotImplementedException(); }
		}
	}

}
