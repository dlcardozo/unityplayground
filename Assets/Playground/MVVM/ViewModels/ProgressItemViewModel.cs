using System;
using Playground.ViewModels;
using Playground.ViewModels.Repositories;
using UniRx;

namespace Playground.MVVM.ViewModels
{
    [Serializable]
    public class ProgressItemViewModel : RuntimeViewModel
    {
        public IntReactiveProperty Initial = new IntReactiveProperty(0);
        public IntReactiveProperty Final = new IntReactiveProperty(0);

        public ProgressItemViewModel(PropertyResolverRepository propertyResolverRepository) : base(propertyResolverRepository)
        {
        }
    }
}