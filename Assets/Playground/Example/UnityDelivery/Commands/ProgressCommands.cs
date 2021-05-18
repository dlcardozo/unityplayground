using Playground.Example.Domain.Actions;
using Playground.Example.UnityDelivery.DataSources;
using UnityEngine;
using UniRx;

namespace Playground.Example.UnityDelivery.Commands
{
    [CreateAssetMenu(menuName = "Playground/Commands/Progress")]
    public class ProgressCommands : ScriptableObject
    {
        [SerializeField] 
        ProgressDataSource progressDataSource;
        
        public void IncrementProgress() =>
            new IncrementProgress()
                .Do(progressDataSource.Get())
                .Subscribe(progress => progressDataSource.Set(progress));
    }
}