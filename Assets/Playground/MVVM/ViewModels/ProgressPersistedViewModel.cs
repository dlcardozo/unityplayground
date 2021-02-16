using System.Collections.Generic;
using Playground.Infrastructure;
using Playground.ViewModels;
using UniRx;
using UnityEngine;

namespace Playground.MVVM.ViewModels
{
    [CreateAssetMenu(menuName = "Playground/DataSources/Progress")]
    public class ProgressPersistedViewModel : PersistedViewModel
    {
        public IntReactiveProperty Progress = new IntReactiveProperty(0);
        public StringReactiveProperty Name = new StringReactiveProperty(string.Empty);
        
        public List<ProgressItemViewModel> Items;

        public ProgressPersistedViewModel() : base(new FixedPropertyResolverRepository())
        {
            Items = new List<ProgressItemViewModel>()
            {
                new ProgressItemViewModel(new FixedPropertyResolverRepository()),
                new ProgressItemViewModel(new FixedPropertyResolverRepository())
            };
        }
    }
}