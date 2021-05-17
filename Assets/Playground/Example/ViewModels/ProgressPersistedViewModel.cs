using Playground.Framework;
using Playground.Infrastructure;
using UniRx;
using UnityEngine;

namespace Playground.Example.ViewModels
{
    [CreateAssetMenu(menuName = "Playground/DataSources/Progress")]
    public class ProgressPersistedViewModel : PersistedViewModel
    {
        public IntReactiveProperty Progress = new IntReactiveProperty(0);
        public StringReactiveProperty Name = new StringReactiveProperty(string.Empty);
        
        public ProgressPersistedViewModel() : base(new FixedPropertyResolverRepository())
        {
        }
    }
}