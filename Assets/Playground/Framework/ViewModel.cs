using Playground.Framework.Repositories;
using Playground.Framework.Wiring;
using UniRx;
using UnityEngine;

namespace Playground.Framework
{
    public class ViewModel : ScriptableObject
    {
        public ISubject<PropertyChanged> onPropertyChanged { get; }

        readonly CompositeDisposable disposables = new CompositeDisposable();
        readonly ViewModelWiring viewModelWiring;

        public ViewModel(PropertyResolverRepository propertyResolverRepository)
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