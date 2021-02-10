using System;
using System.Reflection;
using UniRx;

namespace Playground.ViewModels.Resolvers
{
    public class IntReactivePropertyResolver : PropertyResolver
    {
        public object GetValue(FieldInfo fieldInfo,  object viewModel) => 
            ((IReactiveProperty<int>) fieldInfo.GetValue(viewModel)).Value;

        public IDisposable Subscribe(FieldInfo fieldInfo, object viewModel, Action<string, object> execute) => 
            ((IReactiveProperty<int>) fieldInfo.GetValue(viewModel))
                .Subscribe(value => execute(fieldInfo.Name, value));
    }
}