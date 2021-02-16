using System;
using Playground.ViewModels.Repositories;
using Playground.ViewModels.Wiring;
using UniRx;

namespace Playground.ViewModels
{
    [Serializable]
    public class RuntimeViewModel : ViewModel
    {
        public ISubject<PropertyChanged> onPropertyChanged { get; set; }

        readonly CompositeDisposable disposables = new CompositeDisposable();
        readonly ViewModelWiring viewModelWiring;

        public RuntimeViewModel(PropertyResolverRepository propertyResolverRepository)
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