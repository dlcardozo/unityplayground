using System;

namespace Playground.ViewModels.Wiring
{
    public struct WireField
    {
        public string Field;
        public IDisposable Subscription;
        
        public static WireField Empty => new WireField();
    }
}