using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Playground.ViewModels.Resolvers;
using Playground.ViewModels.Wiring;
using UniRx;
using UnityEngine;

namespace Playground.ViewModels
{
    public class PersistedViewModel : ScriptableObject, ViewModel
    {
        
        public ISubject<PropertyChanged> onPropertyChanged { get; set; }

        readonly CompositeDisposable disposables = new CompositeDisposable();

        public PersistedViewModel()
        {
            onPropertyChanged = new Subject<PropertyChanged>();
            
            var viewModelWiring = new ViewModelWiring(this, new InMemoryPropertyResolverRepository());
            viewModelWiring.Wire();
        }

        void OnDestroy() => disposables.Clear();

        public object GetValueOf(string viewModelProperty)
        {
            throw new NotImplementedException();
            // var viewModelField = fields[viewModelProperty];
            // return propertiesResolver[viewModelField.FieldType]
            //     .GetValue(viewModelField, this);
        }

        void Notify(string property, object newValue) => 
            onPropertyChanged.OnNext(new PropertyChanged(property, newValue));

        // public void DoWiring()
        // {
        //     foreach (var fieldTuple in fields)
        //         if (propertiesResolver.ContainsKey(fieldTuple.Value.FieldType))
        //             SubscribeFieldChanges(fieldTuple);
        // }

        // void SubscribeFieldChanges(KeyValuePair<string, FieldInfo> keyValuePair) =>
        //     disposables.Add(propertiesResolver[keyValuePair.Value.FieldType]
        //         .Subscribe(keyValuePair.Value, this, Notify));
    }
}