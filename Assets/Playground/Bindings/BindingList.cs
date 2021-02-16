using System.Collections.Generic;
using Playground.MVVM.ViewModels;
using Playground.ViewModels;
using UniRx;
using UnityEngine;

namespace Playground.Bindings
{
    public class BindingList : MonoBehaviour
    {
        public PersistedViewModel ViewModel;
        public string ViewModelProperty;
        public BindingListItem Template;
        public GameObject Container;
        
        
        CompositeDisposable disposables = new CompositeDisposable();

        public void OnValidate() => Initialize();

        void Initialize()
        {
            if (!IsValid()) return;
            
            disposables.Clear();

            InstantiateTemplates();
        }

        void InstantiateTemplates()
        {
            ((List<ProgressItemViewModel>)ViewModel.GetValueOf(ViewModelProperty))
                .ForEach(x =>
                {
                    var child = Instantiate(Template, Container.transform);
                    child.ViewModel = x;
                    child.gameObject.SetActive(true);
                });
        }

        bool IsValid() => 
            ViewModel != null && 
            !string.IsNullOrEmpty(ViewModelProperty);
    }
}