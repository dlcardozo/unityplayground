using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Playground.ViewModels.Resolvers;
using UniRx;
using UnityEngine;

namespace Playground.ViewModels
{
    public class ViewModel : ScriptableObject
    {
        Dictionary<string, FieldInfo> fields;
        public ISubject<PropertyChanged> onPropertyChanged = new Subject<PropertyChanged>();

        readonly CompositeDisposable disposables = new CompositeDisposable();
        readonly Dictionary<Type, PropertyResolver> propertiesResolver;

        public ViewModel()
        {
            propertiesResolver = GetPropertiesResolver();

            fields = GetType()
                .GetFields()
                .ToDictionary(field => field.Name);
            
            BindFields();
        }

        void OnDestroy() => disposables.Clear();

        public object GetValueOf(string viewModelProperty)
        {
            var viewModelField = fields[viewModelProperty];
            return propertiesResolver[viewModelField.FieldType]
                .GetValue(viewModelField, this);
        }

        void BindFields()
        {
            foreach (var fieldTuple in fields)
                if (propertiesResolver.ContainsKey(fieldTuple.Value.FieldType))
                    SubscribeFieldChanges(fieldTuple);
        }

        void SubscribeFieldChanges(KeyValuePair<string, FieldInfo> keyValuePair) =>
            disposables.Add(propertiesResolver[keyValuePair.Value.FieldType]
                .Subscribe(keyValuePair.Value, this, Notify));

        void Notify(string property, object newValue) => 
            onPropertyChanged.OnNext(new PropertyChanged(property, newValue));

        static Dictionary<Type, PropertyResolver> GetPropertiesResolver() =>
            new Dictionary<Type, PropertyResolver>()
            {
                {typeof(IntReactiveProperty), new IntReactivePropertyResolver()}
            };
    }
}