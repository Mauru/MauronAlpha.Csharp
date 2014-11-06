using MauronAlpha.ExplainingCode;
using MauronAlpha.ProjectTypes;

namespace MauronAlpha {

	//An OS is the environment a program runs in
    public abstract class OS:Definition{
		//the GUI
        public object frontend;
		//the executing object
        public object backend;
		//The attached device
		public Device Device;

        public abstract void Start(ProgramInstance program);
    }

	//A Programm which serves as an interface to the OS
	public abstract class OSWrapper:OS {
		
	}
}
