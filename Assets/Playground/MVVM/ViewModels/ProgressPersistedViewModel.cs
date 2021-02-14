using Playground.MVVM.Domain.Actions;
using Playground.ViewModels;
using UniRx;
using UnityEngine;

namespace Playground.MVVM.ViewModels
{
    [CreateAssetMenu(menuName = "Playground/DataSources/Progress")]
    public class ProgressPersistedViewModel : PersistedViewModel
    {
        readonly IncrementProgress incrementProgress;

        public IntReactiveProperty Progress = new IntReactiveProperty(0);
        public StringReactiveProperty Name = new StringReactiveProperty(string.Empty);

        public ProgressPersistedViewModel()
        {
        }
    }
}