using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Playground.ViewModels.Repositories;

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

        public List<WireField> Wire(Action<string, object> doOnPropertySubscribe)
        {
            var result = new List<WireField>();

            foreach (var fieldTuple in fields)
            {
                var propertyResolver = propertyResolverRepository.GetBy(fieldTuple.Value.FieldType);

                if (propertyResolver != null)
                    result.Add(CreateWireField(doOnPropertySubscribe, fieldTuple, propertyResolver));
            }
            
            return result;
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

        public object GetValueOf(string property) =>
            propertyResolverRepository
                .GetBy(fields[property].FieldType)
                .GetValue(fields[property], viewModel);
    }
}