using UniRx;

namespace Playground.Framework
{
    public interface ViewModel
    {
        ISubject<PropertyChanged> onPropertyChanged { get; set; }

        object GetValueOf(string viewModelProperty);
    }
}