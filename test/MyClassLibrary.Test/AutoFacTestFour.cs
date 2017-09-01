
using Xunit;
using Autofac;
namespace MyClassLibrary.Test
{
    public class AutoFacTestFour
    {
        interface ICalcultor
        {
            int cal(int i, int j);
        };

        class CalCulatorOne : ICalcultor
        {
            public int cal(int i, int j)
            {
                return i + j;
            }
        }

        class MyCalCulator
        {
            ICalcultor calcultor;

            public MyCalCulator(ICalcultor icalcultor)
            {
                this.calcultor = icalcultor;
            }

            public int cal(int i, int j)
            {
                return calcultor.cal(i, j);
            }
        }

        [Fact]
        public void should_return_type()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new CalCulatorOne()).As<ICalcultor>();

            IContainer container = builder.Build();

            Assert.Equal(typeof(CalCulatorOne),container.Resolve<ICalcultor>().GetType());
        }

        [Fact]
        public void should_return_sum()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new CalCulatorOne()).As<ICalcultor>();
            builder.RegisterType<MyCalCulator>();



            IContainer container = builder.Build();

            Assert.Equal(4, container.Resolve<MyCalCulator>().cal(2, 2));
        }
    }
}