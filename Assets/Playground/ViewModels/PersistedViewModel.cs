using Playground.ViewModels.Infrastructure;
using Playground.ViewModels.Wiring;
using UniRx;
using UnityEngine;

namespace Playground.ViewModels
{
    public class PersistedViewModel : ScriptableObject, ViewModel
    {
        public ISubject<PropertyChanged> onPropertyChanged { get; set; }

        readonly CompositeDisposable disposables = new CompositeDisposable();
        readonly ViewModelWiring viewModelWiring;

        public PersistedViewModel()
        {
            onPropertyChanged = new Subject<PropertyChanged>();
            
            viewModelWiring = new ViewModelWiring(this, new FixedPropertyResolverRepository());
            
            viewModelWiring
                .Wire(NotifyPropertyChange)
                .ForEach(wireField => disposables.Add(wireField.Subscription));
        }

        void OnDestroy() => disposables.Clear();

        public object GetValueOf(string viewModelProperty) => viewModelWiring.GetValueOf(viewModelProperty);

        void NotifyPropertyChange(string property, object newValue) => 
            onPropertyChanged.OnNext(new PropertyChanged(property, newValue));
    }
}