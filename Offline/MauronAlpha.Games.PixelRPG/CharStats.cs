using MauronAlpha;

namespace MauronAlpha.Games.PixelRPG.Attributes {
	
	//mind values

	public class CharAlignment:MauronCode_dataobject {
		public string Name = "Investment";
		public string Description = "";
		public double Value=150;
		public string[]Status=new string[3]{"malign","apathetic","unconditional love"};
		public short Range = 300;
	}

	public class CharPredictability : MauronCode_dataobject {
		public string Name = "Predictability";
		public string Description = "";
		public double Value=150;
		public string[]Status=new string[3]{"emotional","egocentric","cold"};
		public short Range=300;
	}

	//core values for skill check results

	public class CharFate : MauronCode_dataobject {
		public string Name="Fate";
		public string Description = "";
		public double Value=150;
		public string[]Status=new string[3]{"loose","stalemate","win"};
		public short Range=300;
	}
	public class CharResult : MauronCode_dataobject {
		public string Name="Result";
		public string Description = "";
		public double Value=150;
		public string[]Status=new string[3]{"catastrophic","regular","critical"};
		public short Range=300;
	}

	public class CharOutlook : MauronCode_dataobject {
		public string Description = "";
		public double Value=150;
		public string[]Status=new string[3]{"grim","undecided","positive"};
		public short Range=300;
	}

	//body values

	public class CharFocus : MauronCode_dataobject {
		public string Name = "Focus";
		public string Description = "";
		public double Value=150;
		public string[] Status=new string[3]{"mind","senses","body"};
		public short Range=300;
		public StatModifier[] Connections=new StatModifier[]{
			new StatModifier("Focus.mind","Focus.body",-30),
			new StatModifier("Focus.mind","Focus.senses",-30),
			new StatModifier("Focus.senses","Focus.mind",-30),
			new StatModifier("Focus.senses","Focus.body",-30),
			new StatModifier("Focus.body","Focus.mind",-30),
			new StatModifier("Focus.body","Focus.senses",-30)			
		};
	}
	public class CharHealth : MauronCode_dataobject {
		public string Name="Health";
		public string Description="";
		public double Value=150;
		public string[] Status=new string[3] {"diseased","normal","ecstatic"};
		public short Range=300;
	}

	public class StatModifier : MauronCode_dataobject {
		public string Source;
		public string Target;
		public double Influence;
		public StatModifier(string source,string target,double influence){
			Source=source;
			Target=target;
			Influence=influence;
		}
	}

	public class CharAttributes : MauronCode_dataobject {
		public string[] Ideas=new string[4]{"stamina","strength","speed","intelligence"};
	}
	public class CharModifiers : MauronCode_dataobject {
		public string[] Ideas=new string[2]{"skill","data"}; 
	}
}
