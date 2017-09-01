using Xunit;
using Autofac;
namespace MyClassLibrary.Test
{
    public class AutoFacTestFirth
    {
        interface People
        {
            string getName(string name);
        }

        class Female : People
        {
            public string getName(string name)
            {
                return name;
            }
        }

        class Myself
        {
            People people;

            public Myself(People p)
            {
                this.people = p;
            }
     
            public string getMyName(string name)
            {
                return people.getName(name);
            }
        }

        [Fact]
        public void should_return_name()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new Female()).As<People>();

            IContainer contaimer = builder.Build();
            var name = "dan";
            Assert.Equal(name,contaimer.Resolve<People>().getName(name));
        }

        [Fact]
        public void should_return_name_by()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new Female()).As<People>();
            builder.Register(c => new Myself(c.Resolve<People>()));

            IContainer container = builder.Build();
            var name = "feng";
            Assert.Equal(name,container.Resolve<Myself>().getMyName(name));
        }
    }
}