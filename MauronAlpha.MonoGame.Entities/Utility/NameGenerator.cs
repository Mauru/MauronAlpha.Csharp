﻿
namespace MauronAlpha.MonoGame.Entities.Utility {
	
	public class NameGenerator:EntityComponent {

		public static string Create(LinguisticTool type) { 
			return type.New;
		}

	}

	public class LinguisticTool : EntityComponent {

		public string New {
			get {
				return "TODO:Somehow generate names";
			}
		}

	}

	public class NameFormulae : EntityComponent {

		public static LinguisticTool Site {
			get {
				return new LinguisticTool();
			}
		}

	}

}
