using NSubstitute;

namespace Playground.ViewModels.Tests
{
    public static class ViewModelMother
    {
        public static WireableViewModel AWireableViewModel()
        {
            return Substitute.For<WireableViewModel>();
        }
    }
}