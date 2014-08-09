using MauronAlpha;
using MauronAlpha.Projects;

namespace MauronAlpha.ProjectTypes {

	// A program
    public abstract class Program:MauronCode_project {
		//The device the program is running on
        public Device Device;

		//The OS of the device
        public OS Os;

		//The Instance where the program is running
        public ProgramInstance Self;

		//constructor
		public Program (string name) : base(ProjectType_generic.Instance, name) { }

		//Start a program
        public ProgramInstance Start(Program program,Device device, OS os){
            Device = device;
            Os = os;
            Self = new ProgramInstance(program,Name);

            //open a sample window with the welcome message
            Os.Start(Self);

            return Self;
        }
	}

	//An instance of a program
    public class ProgramInstance:Program {
        public Program Program;
        public ProgramInstance(Program program,string name):base(name) {
            Program = program;
			Self=this;
        }
    }
}
