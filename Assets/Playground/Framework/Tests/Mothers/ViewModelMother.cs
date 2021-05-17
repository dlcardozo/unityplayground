using UniRx;

namespace Playground.Framework.Tests.Mothers
{
    public static class ViewModelMother
    {
        public static ViewModel SomeViewModel()
        {
            return new SomeViewModel();
        }
    }
    
    public class SomeViewModel : ViewModel
    {
        public ReactiveProperty<int> SomeProperty = new ReactiveProperty<int>(0);
        public ISubject<PropertyChanged> onPropertyChanged { get; set; }

        public object GetValueOf(string viewModelProperty) => throw new System.NotImplementedException();
    }
}