using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Playground.Framework.Properties
{
    public class PropertyHelper
    {
        public static readonly ConcurrentDictionary<Type, PropertyHelper[]> cache = 
            new ConcurrentDictionary<Type, PropertyHelper[]>();

        public string Name { get; set; }
        public Type PropertyType { get; set; }
        
        public Func<object, object> Getter { get; set; }
        public Action<object, object> Setter { get; set; }

        static readonly MethodInfo CallInnerDelegateMethodVoid =
            typeof(PropertyHelper).GetMethod(nameof(CallInnerDelegateVoid), BindingFlags.NonPublic | BindingFlags.Static);

        static Action<object, object> CallInnerDelegateVoid<TClass, TValue>(Action<TClass, TValue> deleg) => 
            (instance, val) => deleg((TClass) instance, (TValue)val);

        static Action<object, object> SetterDelegate(PropertyInfo property, Type declaringClass,
            Type typeOfProperty)
        {
            var method = property.SetMethod;

            if (method == null)
                return (o, o1) => { };

            var methodDelegateType = typeof(Action<,>).MakeGenericType(declaringClass, typeOfProperty);
            var methodDelegate = method.CreateDelegate(methodDelegateType);
            var callInnerGenericMethodWithTypes = CallInnerDelegateMethodVoid
                .MakeGenericMethod(declaringClass, typeOfProperty);
            var newSetter =
                (Action<object, object>) callInnerGenericMethodWithTypes.Invoke(null,
                    new[] {methodDelegate});
            return newSetter;
        }

        static readonly MethodInfo CallInnerDelegateMethod =
            typeof(PropertyHelper).GetMethod(nameof(CallInnerDelegate), BindingFlags.NonPublic | BindingFlags.Static);

        static Func<object, object> CallInnerDelegate<TClass, TResult>(Func<TClass, TResult> deleg)
            => instance => deleg((TClass)instance);

        static Func<object, object> GetterDelegate(PropertyInfo property, Type declaringClass, Type typeOfResult)
        {
            var getMethod = property.GetMethod;

            if (getMethod == null)
                return o => o;

            var getMethodDelegateType = typeof(Func<,>).MakeGenericType(declaringClass, typeOfResult);
            var getMethodDelegate = getMethod.CreateDelegate(getMethodDelegateType);
            var callInnerGenericMethodWithTypes = CallInnerDelegateMethod
                .MakeGenericMethod(declaringClass, typeOfResult);
            var newGetter =
                (Func<object, object>) callInnerGenericMethodWithTypes.Invoke(null,
                    new[] {getMethodDelegate});
            return newGetter;
        }

        public static PropertyHelper[] GetProperties(Type type)
            => cache
                .GetOrAdd(type, _ => type
                    .GetProperties()
                    .Select(property => new PropertyHelper
                    {
                        Name = property.Name,
                        PropertyType = property.PropertyType,
                        Getter = GetterDelegate(property, property.DeclaringType, property.PropertyType),
                        Setter = SetterDelegate(property, property.DeclaringType, property.PropertyType)
                    })
                    .ToArray());
    }
}