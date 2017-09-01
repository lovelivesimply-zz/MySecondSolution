using Xunit;
using Autofac;
namespace MyClassLibrary.Test
{
    public class AutoFacTestEight
    {
        interface Animal
        {
            string name(string name);
        };

        class Duck : Animal
        {
            public string name(string name)
            {
                return "My name is " + name;
            }
        }

        class MyDuck
        {
            readonly Animal animal;//readonly:????

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
        public void should_return_name()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new Duck()).As<Animal>();

            IContainer container = builder.Build();
            Assert.Equal("My name is duck",container.Resolve<Animal>().name("duck"));
        }

        [Fact]
        public void should()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new Duck()).As<Animal>();
            builder.Register(c => new MyDuck(c.Resolve<Animal>()));
            IContainer container = builder.Build();

            Assert.Equal("My name is duck", container.Resolve<MyDuck>().getName("duck"));

        }
    }
}