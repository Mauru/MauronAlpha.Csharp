
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.Forms.Units;

using Microsoft.Xna.Framework.Graphics;

namespace MauronAlpha.MonoGame.Resources {
	public class GameFont : GameResource {
		string Name;
		string STR_resourceCode;
		public string ResourceCode { get { return STR_resourceCode; } }
		GameFontStyle FontStyle;

		public GameFont(string name, string code, GameFontStyle style)	: base(ResourceType_gameFont.Instance) {
			FontStyle = style;
			STR_resourceCode = code;
			Name = name;
		}
		public Vector2d MeasureText(FormUnit_textField text, ResourceManager resources) {
			Vector2d result = new Vector2d();
			Lines ll = text.Lines;
			foreach (Line l in ll) {
				Vector2d size = MeasureText(l, resources);
				result.Add(size);
			}
			return result;
		}
		public Vector2d MeasureText(Line line, ResourceManager resourceManager) {
			Vector2d minMax = new Vector2d();
			Characters cc = line.Characters;
			Vector2d result = new Vector2d(0, 0);
			foreach (Character c in cc) {
				Vector2d charSize = MeasureCharacter(c, resourceManager);
				if (charSize.Y > result.Y)
					result.SetY(charSize.Y);
				result.Add(charSize.X, 0);
			}
			return result;
		}
		public Vector2d MeasureCharacter(Character c, ResourceManager resourceManager) {
			if (c.IsEmpty)
				return new Vector2d();
			if (c.IsVirtual)
				return new Vector2d();
			return new Vector2d(1, 1);
		}
	}

	public class ResourceType_gameFont:ResourceType {
		public override string Name { 
			get{ return "GameFont"; }
		}
		public static ResourceType_gameFont Instance { 
			get { 
				return new ResourceType_gameFont(); 
			}
		}
	}
}
