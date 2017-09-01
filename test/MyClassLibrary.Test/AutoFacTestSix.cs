using Xunit;
using Autofac;
namespace MyClassLibrary.Test
{
    public class AutoFacTestSix
    {
        interface Animal
        {
            string name(string name);
        };

        class Duck:Animal
        {
            public string name(string name)
            {
                return "My name is " + name;
            }
        }

        class MyDuck
        {
            Animal animal;

            public MyDuck(Animal ani)
            {
                this.animal = ani;
            }

            public string getName(string name)
            {
                return animal.name(name);
            }
        }

        [Fact]
        public void should_get_name()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new Duck()).As<Animal>();

            IContainer container = builder.Build();

            Assert.Equal("My name is duck",container.Resolve<Animal>().name("duck"));
        }

        [Fact]
        public void should_get_duck_name()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new Duck()).As<Animal>();
            builder.Register(_ => new MyDuck(_.Resolve<Animal>()));

            IContainer container = builder.Build();

            Assert.Equal("My name is Duck", container.Resolve<MyDuck>().getName("Duck"));
        }

        [Fact]
        public void should_get_duck_name_test()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new MyDuck(_.Resolve<Animal>()));
            builder.Register(_ => new Duck()).As<Animal>();

            IContainer container = builder.Build();

            Assert.Equal("My name is Duck", container.Resolve<MyDuck>().getName("Duck"));
        }
    }
}