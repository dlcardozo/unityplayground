using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Playground.ViewModels.Repositories;
using Playground.ViewModels.Resolvers;

namespace Playground.ViewModels.Wiring
{
    public class ViewModelWiring
    {
        readonly ViewModel viewModel;
        readonly PropertyResolverRepository propertyResolverRepository;
        
        Dictionary<string, FieldInfo> fields;

        public ViewModelWiring(ViewModel viewModel, PropertyResolverRepository propertyResolverRepository)
        {
            this.viewModel = viewModel;
            this.propertyResolverRepository = propertyResolverRepository;

            fields = this.viewModel.GetType()
                .GetFields()
                .ToDictionary(field => field.Name);
        }

        public object GetValueOf(string property) =>
            propertyResolverRepository
                .GetBy(fields[property].FieldType)
                .GetValue(fields[property], viewModel);

        public List<WireField> Wire(Action<string, object> doOnPropertySubscribe) =>
            fields
                .ToList()
                .Select(fieldTuple => GetWireFieldFromField(doOnPropertySubscribe, fieldTuple))
                .Where(wireField => !wireField.Equals(WireField.Empty))
                .ToList();

        WireField GetWireFieldFromField(Action<string, object> doOnPropertySubscribe, KeyValuePair<string, FieldInfo> fieldTuple)
        {
            var propertyResolver = propertyResolverRepository.GetBy(fieldTuple.Value.FieldType);

            return propertyResolver != null
                ? CreateWireField(doOnPropertySubscribe, fieldTuple, propertyResolver)
                : WireField.Empty;
        }

        WireField CreateWireField(
            Action<string, object> doOnPropertySubscribe,
            KeyValuePair<string, FieldInfo> fieldTuple, 
            PropertyResolver propertyResolver
        ) => new WireField
            {
                Field = fieldTuple.Key,
                Subscription =
                    propertyResolver.SubscribeProperty(fieldTuple.Value, viewModel, doOnPropertySubscribe)
            };
    }
}