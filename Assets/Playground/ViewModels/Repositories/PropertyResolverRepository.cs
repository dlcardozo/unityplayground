using System;
using System.Collections.Generic;
using System.Reflection;
using Playground.ViewModels.Resolvers;
using UniRx;

namespace Playground.ViewModels
{
    public interface PropertyResolverRepository
    {
        PropertyResolver GetBy(Type type);

        IDisposable SubscribeProperty(Type propertyType, FieldInfo property, ViewModel viewModel, Action<string,object> doAction);
    }

    public class InMemoryPropertyResolverRepository : PropertyResolverRepository
    {
        Dictionary<Type, PropertyResolver> propertiesResolver;

        public InMemoryPropertyResolverRepository()
        {
            propertiesResolver = new Dictionary<Type, PropertyResolver>
            {
                {typeof(IntReactiveProperty), new ReactivePropertyResolver<int>()},
                {typeof(FloatReactiveProperty), new ReactivePropertyResolver<float>()},
                {typeof(StringReactiveProperty), new ReactivePropertyResolver<string>()},
                {typeof(BoolReactiveProperty), new ReactivePropertyResolver<bool>()},
                {typeof(LongReactiveProperty), new ReactivePropertyResolver<long>()},
                {typeof(DoubleReactiveProperty), new ReactivePropertyResolver<double>()}
            };
        }

        public PropertyResolver GetBy(Type type) => 
            propertiesResolver.ContainsKey(type) 
                ? propertiesResolver[type] 
                : null;

        public IDisposable SubscribeProperty(
            Type propertyType, 
            FieldInfo property,
            ViewModel viewModel,
            Action<string, object> doAction
        ) => GetBy(propertyType).SubscribeProperty(property, viewModel, doAction);
    }
}