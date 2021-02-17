using System;
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

        public void Start() => Initialize();

        void Initialize()
        {
            if (!IsValid()) return;

            disposables.Clear();

            InstantiateTemplates();
        }

        void InstantiateTemplates()
        {
            Container.DestroyChildren();

            ((List<ProgressItemViewModel>) ViewModel.GetValueOf(ViewModelProperty))
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

    public static class GameObjectExtensions
    {
        public static void DestroyChildren(this GameObject source)
        {
            int childs = source.transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(source.transform.GetChild(i).gameObject);
            }
        }
    }
}