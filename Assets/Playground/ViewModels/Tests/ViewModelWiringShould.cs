using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Playground.ViewModels.Resolvers;
using Playground.ViewModels.Wiring;
using UniRx;
using static Playground.ViewModels.Tests.ViewModelMother;

namespace Playground.ViewModels.Tests
{
    [TestFixture]
    public class ViewModelWiringShould
    {
        ViewModelWiring viewModelWiring;
        ViewModel viewModel;
        List<WireField> wireResult;
        PropertyResolverRepository propertyResolverRepository;
        PropertyResolver somePropertyResolver;

        [SetUp]
        public void Setup()
        {
            viewModel = SomeViewModel();
            propertyResolverRepository = Substitute.For<PropertyResolverRepository>();
            somePropertyResolver = Substitute.For<PropertyResolver>();
            viewModelWiring = new ViewModelWiring(viewModel, propertyResolverRepository);
        }
        
        [Test]
        public void WireViewModelProperty()
        {
            GivenASupportedProperty();
            WhenWire();
            ThenWireResultIs(new List<WireField>() { new WireField { Field = "SomeProperty"}});
        }

        [Test]
        public void NotWireUnknownProperties()
        {
            GivenNoSupportedProperty();
            WhenWire();
            ThenWireResultIs(new List<WireField>());
        }

        [Test]
        public void SubscribeToPropertyChange()
        {
            GivenAPropertyResolver();
            WhenWire();
            ThenSubscribePropertyIsCalled();
        }

        void GivenAPropertyResolver()
        {
            propertyResolverRepository
                .GetBy(typeof(ReactiveProperty<int>))
                .Returns(somePropertyResolver);
        }

        void GivenNoSupportedProperty() =>
            propertyResolverRepository
                .GetBy(typeof(ReactiveProperty<int>))
                .ReturnsNull();

        void GivenASupportedProperty() =>
            propertyResolverRepository
                .GetBy(typeof(ReactiveProperty<int>))
                .Returns(new ReactivePropertyResolver<int>());

        void WhenWire() => wireResult = viewModelWiring.Wire();

        void ThenWireResultIs(List<WireField> expected) => 
            Assert.IsTrue(wireResult.SequenceEqual(expected));

        void ThenSubscribePropertyIsCalled() =>
            somePropertyResolver.Received(1)
                .SubscribeProperty(
                    Arg.Any<FieldInfo>(),
                    Arg.Any<ViewModel>(),
                    Arg.Any<Action<string, object>>()
                );
    }
}