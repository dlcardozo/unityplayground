using Playground.Framework.Repositories;
using Playground.Framework.Wiring;
using UniRx;
using UnityEngine;

namespace Playground.Framework
{
    public class PersistedViewModel : ScriptableObject, ViewModel
    {
        public ISubject<PropertyChanged> onPropertyChanged { get; set; }

        readonly CompositeDisposable disposables = new CompositeDisposable();
        readonly ViewModelWiring viewModelWiring;

        public PersistedViewModel(PropertyResolverRepository propertyResolverRepository)
        {
            onPropertyChanged = new Subject<PropertyChanged>();
            
            viewModelWiring = new ViewModelWiring(this, propertyResolverRepository);
            
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