using System;
using Playground.Framework.Properties;

namespace Playground.Framework.Repositories
{
    public interface PropertyResolverRepository
    {
        PropertyResolver GetBy(Type type);
    }
}