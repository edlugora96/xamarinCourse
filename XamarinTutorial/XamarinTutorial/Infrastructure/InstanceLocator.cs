using XamarinTutorial.ViewModels;

namespace XamarinTutorial.Infrastructure
{
    public class InstanceLocator
    {
     
        public MainViewModel Main { get; set; }
   
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
