using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute.Core;
using Playground.ViewModels.Resolvers;
using UniRx;

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

        public List<WireField> Wire()
        {
            var result = new List<WireField>();

            foreach (var fieldTuple in fields)
            {
                var propertyResolver = propertyResolverRepository.GetBy(fieldTuple.Value.FieldType);

                if (propertyResolver != null)
                {
                    propertyResolver.SubscribeProperty(fieldTuple.Value, viewModel, (s, o) => { });
                    result.Add(new WireField { Field = fieldTuple.Key});
                }
            }
                

            return result;
        }
    }

    public struct WireField
    {
        public string Field;
    }
}