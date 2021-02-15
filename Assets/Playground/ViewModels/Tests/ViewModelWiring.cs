namespace Playground.ViewModels.Tests
{
    public class ViewModelWiring
    {
        readonly WireableViewModel wireableViewModel;

        public ViewModelWiring(WireableViewModel wireableViewModel)
        {
            this.wireableViewModel = wireableViewModel;
        }

        public void Wire()
        {
            wireableViewModel.DoWiring();
        }
    }
}