using Playground.Example.Domain.ValueObjects;
using UniRx;
using UnityEngine;

namespace Playground.Example.UnityDelivery.DataSources
{
    [CreateAssetMenu(menuName = "Playground/DataSources/Progress")]
    public class ProgressDataSource : ScriptableObject
    {
        public ReactiveProperty<Progress> Current = new ReactiveProperty<Progress>(Progress.Empty);

        public Progress Get() => Current.Value;
        
        public void Set(Progress updatedProgress) => Current.Value = updatedProgress;
    }
}