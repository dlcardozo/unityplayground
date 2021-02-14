using System.Reflection;
using Playground.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using static System.Convert;

namespace Playground.Bindings
{
    public class BindingComponent : MonoBehaviour
    {
        [FormerlySerializedAs("ViewModel")] public PersistedViewModel persistedViewModel;
        public string ViewModelProperty;

        public GameObject Target;
        public Component TargetComponent;
        public string TargetComponentProperty;
        
        CompositeDisposable disposables = new CompositeDisposable();
        PropertyInfo targetComponentPropertyInfo;

        void OnDestroy() => disposables.Clear();

        public void OnValidate()
        {
            Initialize();
        }

        void Initialize()
        {
            disposables.Clear();
            if (!IsValid()) return;
            
            UpdateTargetComponent(new PropertyChanged(ViewModelProperty, persistedViewModel.GetValueOf(ViewModelProperty)));
            BindToViewModel();
        }

        void BindToViewModel() =>
            persistedViewModel.onPropertyChanged
                .Where(propertyChanged => propertyChanged.Property.Equals(ViewModelProperty))
                .Subscribe(UpdateTargetComponent)
                .AddTo(disposables);

        void UpdateTargetComponent(PropertyChanged changed)
        {
            if (GetTargetComponentPropertyInfo() != null)
                GetTargetComponentPropertyInfo()
                    .SetValue(TargetComponent, ChangeType(changed.Value, GetTargetComponentPropertyInfo().PropertyType));
        }

        PropertyInfo GetTargetComponentPropertyInfo() => 
            targetComponentPropertyInfo ?? 
            (targetComponentPropertyInfo = TargetComponent.GetType().GetProperty(TargetComponentProperty));

        bool IsValid() => 
            Target != null && 
            TargetComponent != null && 
            !string.IsNullOrEmpty(TargetComponentProperty) &&
            persistedViewModel != null && 
            !string.IsNullOrEmpty(ViewModelProperty);
    }
}