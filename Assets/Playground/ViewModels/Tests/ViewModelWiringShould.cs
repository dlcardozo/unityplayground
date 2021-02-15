using NSubstitute;
using NUnit.Framework;
using static Playground.ViewModels.Tests.ViewModelMother;

namespace Playground.ViewModels.Tests
{
    [TestFixture]
    public class ViewModelWiringShould
    {
        ViewModelWiring viewModelWiring;
        WireableViewModel wireableViewModel;

        [SetUp]
        public void Setup()
        {
            wireableViewModel = AWireableViewModel();
            viewModelWiring = new ViewModelWiring(wireableViewModel);
        }
        
        [Test]
        public void WireViewModel()
        {
            WhenWire();
            ThenDoWiringIsCalled();
        }

        void WhenWire() => viewModelWiring.Wire();

        void ThenDoWiringIsCalled() => wireableViewModel.Received(1).DoWiring();
    }
}