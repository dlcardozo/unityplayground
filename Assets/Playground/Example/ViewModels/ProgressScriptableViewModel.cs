using System;
using Playground.Example.Domain.Actions;
using Playground.Example.Domain.ValueObjects;
using Playground.Framework;
using Playground.Infrastructure;
using UniRx;
using UnityEngine;

namespace Playground.Example.ViewModels
{
    [CreateAssetMenu(menuName = "Playground/DataSources/Progress")]
    public class ProgressScriptableViewModel : ScriptableViewModel
    {
        public IntReactiveProperty Progress = new IntReactiveProperty(0);
        public StringReactiveProperty Name = new StringReactiveProperty(string.Empty);

        ISubject<Progress> onProgressUpdated;
        
        public ProgressScriptableViewModel() : base(new FixedPropertyResolverRepository())
        {
            onProgressUpdated = new Subject<Progress>();
        }

        void OnEnable()
        {
            onProgressUpdated.Subscribe(progress =>
            {
                Progress.Value = progress.Current;
                Name.Value = $"{progress.Current} / {progress.Total}";
            });
        }

        public void AdvanceProgress() => new IncrementProgress(onProgressUpdated).Do(Progress.Value); 
    }
}