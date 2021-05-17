using System;
using Playground.Framework.Resolvers;

namespace Playground.Framework.Repositories
{
    public interface PropertyResolverRepository
    {
        PropertyResolver GetBy(Type type);
    }
}