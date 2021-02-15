using System;
using System.Reflection;
using UniRx;

namespace Playground.ViewModels.Resolvers
{
    public class ReactivePropertyResolver<T> : PropertyResolver
    {
        public object GetValue(FieldInfo fieldInfo, object viewModel) =>  
            ((IReactiveProperty<T>) fieldInfo.GetValue(viewModel)).Value;

        public IDisposable SubscribeProperty(FieldInfo fieldInfo, object viewModel, Action<string, object> execute) =>
            ((IReactiveProperty<T>) fieldInfo.GetValue(viewModel))
            .Subscribe(value => execute(fieldInfo.Name, value));
    }
}