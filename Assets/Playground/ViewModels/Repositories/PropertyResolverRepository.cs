using System;
using Playground.ViewModels.Resolvers;

namespace Playground.ViewModels.Repositories
{
    public interface PropertyResolverRepository
    {
        PropertyResolver GetBy(Type type);
    }
}