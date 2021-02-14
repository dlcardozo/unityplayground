using UniRx;

namespace Playground.ViewModels
{
    public interface ViewModel
    {
        ISubject<PropertyChanged> onPropertyChanged { get; set; }

        object GetValueOf(string viewModelProperty);
    }
}