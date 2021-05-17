using System.Linq;
using System.Reflection;
using Playground.Framework.Resolvers;
using UniRx;
using UnityEngine;
using static System.Convert;

namespace Playground.Framework.Bindings
{
    public class BindingComponent : MonoBehaviour
    {
        public PersistedViewModel ViewModel;
        public string ViewModelProperty;

        public GameObject Target;
        public Component TargetComponent;
        public string TargetComponentProperty;
        
        CompositeDisposable disposables = new CompositeDisposable();
        PropertyInfo targetComponentPropertyInfo;

        void OnDestroy() => disposables.Clear();

        public void OnValidate() => Initialize();

        void Initialize()
        {
            disposables.Clear();
            if (!IsValid()) return;

            PropertyHelper.GetProperties(TargetComponent.GetType());
            
            UpdateTargetComponent(new PropertyChanged(ViewModelProperty, ViewModel.GetValueOf(ViewModelProperty)));
            BindToViewModel();
        }

        void BindToViewModel() =>
            ViewModel.onPropertyChanged
                .Where(propertyChanged => propertyChanged.Property.Equals(ViewModelProperty))
                .Subscribe(UpdateTargetComponent)
                .AddTo(disposables);

        void UpdateTargetComponent(PropertyChanged changed)
        {
            var updateProperty = PropertyHelper.GetProperties(TargetComponent.GetType())
                .First(property => property.Name.Equals(TargetComponentProperty));
            updateProperty.Setter(TargetComponent, ChangeType(changed.Value, updateProperty.PropertyType));
        }

        bool IsValid() => 
            Target != null && 
            TargetComponent != null && 
            !string.IsNullOrEmpty(TargetComponentProperty) &&
            ViewModel != null && 
            !string.IsNullOrEmpty(ViewModelProperty);
    }
}