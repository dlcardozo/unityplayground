namespace Playground.ViewModels
{
    public struct PropertyChanged
    {
        public readonly string Property;
        public readonly object Value;

        public PropertyChanged(string property, object value)
        {
            Property = property;
            Value = value;
        }
    }
}