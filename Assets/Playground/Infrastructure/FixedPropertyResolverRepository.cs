using System;
using System.Collections.Generic;
using Playground.MVVM.ViewModels;
using Playground.ViewModels;
using Playground.ViewModels.Repositories;
using Playground.ViewModels.Resolvers;
using UniRx;

namespace Playground.Infrastructure
{
    public class FixedPropertyResolverRepository : PropertyResolverRepository
    {
        readonly Dictionary<Type, PropertyResolver> propertiesResolver;

        public FixedPropertyResolverRepository()
        {
            propertiesResolver = new Dictionary<Type, PropertyResolver>
            {
                {typeof(IntReactiveProperty), new ReactivePropertyResolver<int>()},
                {typeof(FloatReactiveProperty), new ReactivePropertyResolver<float>()},
                {typeof(StringReactiveProperty), new ReactivePropertyResolver<string>()},
                {typeof(BoolReactiveProperty), new ReactivePropertyResolver<bool>()},
                {typeof(LongReactiveProperty), new ReactivePropertyResolver<long>()},
                {typeof(DoubleReactiveProperty), new ReactivePropertyResolver<double>()},
            };
        }

        public PropertyResolver GetBy(Type type) => 
            propertiesResolver.ContainsKey(type) 
                ? propertiesResolver[type] 
                : null;
    }
}