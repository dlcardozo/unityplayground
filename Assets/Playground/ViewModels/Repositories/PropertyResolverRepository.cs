using System;
using System.Collections.Generic;
using Playground.ViewModels.Resolvers;
using UniRx;

namespace Playground.ViewModels.Repositories
{
    public interface PropertyResolverRepository
    {
        PropertyResolver GetBy(Type type);
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
    }
}