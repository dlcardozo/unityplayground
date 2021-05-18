using Playground.Example.UnityDelivery.DataSources;
using Playground.Framework;
using Playground.Infrastructure;
using UniRx;
using UnityEngine;

namespace Playground.Example.UnityDelivery.ViewModels
{
    [CreateAssetMenu(menuName = "Playground/ViewModels/Progress")]
    public class ProgressViewModel : ViewModel
    {
        [SerializeField] ProgressDataSource progressDataSource;

        public IntReactiveProperty Progress = new IntReactiveProperty(0);
        public StringReactiveProperty Name = new StringReactiveProperty(string.Empty);

        public ProgressViewModel() : base(new FixedPropertyResolverRepository())
        {
        }

        void OnEnable()
        {
            progressDataSource.Current
                .SkipLatestValueOnSubscribe()
                .Subscribe(progress =>
                {
                    Progress.Value = progress.Current;
                    Name.Value = $"{progress.Current} / {progress.Total}";
                });
        }
    }
}