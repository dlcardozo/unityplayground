using Playground.MVVM.Domain.Actions;
using Playground.ViewModels;
using UniRx;
using UnityEngine;

namespace Playground.MVVM.ViewModels
{
    [CreateAssetMenu(menuName = "Playground/DataSources/Progress")]
    public class ProgressViewModel : ViewModel
    {
        readonly IncrementProgress incrementProgress;

        public IntReactiveProperty Progress = new IntReactiveProperty(0);

        public ProgressViewModel()
        {
        }
    }
}