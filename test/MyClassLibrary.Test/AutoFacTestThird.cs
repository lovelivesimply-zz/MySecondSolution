using Xunit;
using Autofac;
namespace MyClassLibrary.Test
{
    public class AutoFacTestThird
    {
        interface Icalcultor
        {
            int cal(int i, int j);
        };

        class CalCulatorOne : Icalcultor
        {
            public int cal(int i, int j)
            {
                return i + j;
            }
        }

        class MyCalCulator
        {
            Icalcultor calcultor;

            public MyCalCulator(Icalcultor icalcultor)
            {
                this.calcultor = icalcultor;
            }

            public int cal(int i, int j)
            {
                return calcultor.cal(i, j);
            }
        }

        [Fact]
        public void should_get_calculator()
        {
            var builder = new ContainerBuilder();
            builder.Register(_=>new CalCulatorOne()).As<Icalcultor>();

            IContainer container = builder.Build();

            Assert.Equal(typeof(CalCulatorOne),container.Resolve<Icalcultor>().GetType());
        }

        public void should_return_sum()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new CalCulatorOne()).As<Icalcultor>();
           // builder.Register(_ => new MyCalCulator(_.Resolve<Icalcultor>()));

            builder.Register(c => { return new MyCalCulator(c.Resolve<Icalcultor>()); });

            IContainer container = builder.Build();
            Assert.Equal(4, container.Resolve<MyCalCulator>().cal(2, 2));
        }


    }
}