
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Text.Units;

using Microsoft.Xna.Framework.Graphics;

namespace MauronAlpha.MonoGame.Resources {
	public class GameFont:GameResource {
		string  Name;
		string STR_resourceCode;
		public string ResourceCode { get { return STR_resourceCode; } }
		GameFontStyle FontStyle;

		public GameFont(string name, string code, GameFontStyle style) : base(ResourceType_gameFont.Instance) {
			FontStyle = style;
			STR_resourceCode = code;
			Name = name;
		}

		public Vector2d MeasureText(TextUnit_text text, Vector2d maxSize, SpriteFont font) {
			
		}
	}

	public class ResourceType_gameFont:ResourceType {
		public override string Name { 
			get{ return "GameFont"; }
		}
		public static ResourceType_gameFont Instance { 
			get { return new ResourceType_gameFont(); }
		}
	}
}
