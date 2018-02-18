using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Aero.Common
{
    public class DisposableManagerTests
    {
        [Fact]
        public void Register_Single_Test()
        {
            //Arrange
            var disposable1 = Substitute.For<IDisposable>();

            //Act
            var dm = new DisposableManager();
            var disposable2 = dm.Register<IDisposable>(disposable1);
            dm.Dispose();

            //Assert
            disposable1.Received(1).Dispose();
            disposable2.Received(1).Dispose();
            disposable1.Should().Be(disposable2);
        }

        [Fact]
        public void Register_Multiple_Test()
        {
            //Arrange
            var disposable1 = Substitute.For<IDisposable>();
            var disposable2 = Substitute.For<IDisposable>();

            //Act
            var dm = new DisposableManager();
            dm.Register(disposable1, disposable2);
            dm.Dispose();

            //Assert
            disposable1.Received(1).Dispose();
            disposable2.Received(1).Dispose();
        }

        [Fact]
        public void Dispose_Ignores_Exceptions()
        {
            //Arrange
            var disposable1 = Substitute.For<IDisposable>();
            var disposable2 = Substitute.For<IDisposable>();

            disposable1.When(x => x.Dispose()).Throw<Exception>();

            //Act
            var dm = new DisposableManager();
            dm.Register(disposable1, disposable2);
            dm.Dispose();

            //Assert
            disposable1.Received(1).Dispose();
            disposable2.Received(1).Dispose();
        }

        [Fact]
        public void Dispose_Clears_Internal_Collection()
        {
            //Arrange
            var disposable1 = Substitute.For<IDisposable>();

            //Act
            var dm = new DisposableManager();
            dm.Register<IDisposable>(disposable1);
            dm.Dispose();
            dm.Dispose();

            //Assert
            disposable1.Received(1).Dispose();
        }
    }
}
