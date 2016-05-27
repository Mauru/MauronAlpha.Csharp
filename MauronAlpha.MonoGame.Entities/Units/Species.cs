using MauronAlpha.MonoGame.Entities;
using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities.Quantifiers;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Species:EntityComponent {

		SpeciesForm MainForm;
		SpeciesBody Body;

		SpeciesMind Mind;
		Characteristics Characteristics;

		Stances Stances;
		MobilityTypes Mobility;

		Affinities Affinities;

	}

	public abstract class SpeciesForm : EntityComponent {

		public abstract string Name { get; }

	}

	public class SpeciesForm_bacteria : SpeciesForm {

		public override string Name { get { return "Bacteria"; } }

	}

	public class SpeciesForm_gaseous : SpeciesForm {

		public override string Name { get { return "Gaseous"; } }

	}

	public class SpeciesForm_spirit : SpeciesForm {

		public override string Name { get { return "Spirit"; } }

	}

	public class SpeciesForm_slime : SpeciesForm {

		public override string Name { get { return "Slime"; } }

	}

	public class SpeciesForm_element : SpeciesForm {

		public override string Name { get { return "Element"; } }

	}

	public class SpeciesForm_living : SpeciesForm {

		public override string Name { get { return "Living"; } }

	}

	public class SpeciesBody:EntityComponent {

		public Limbs Limbs;

	}
	public class SpeciesMind : EntityComponent {

		Emotions Emotions;

	}

}
