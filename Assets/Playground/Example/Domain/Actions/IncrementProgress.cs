using System;
using Playground.Example.Domain.ValueObjects;
using UniRx;

namespace Playground.Example.Domain.Actions
{
    public class IncrementProgress
    {
        public IObservable<Progress> Do(Progress progress) => 
            Observable.Return(progress.Increment());
    }
}