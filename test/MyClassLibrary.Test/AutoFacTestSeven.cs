using System;
using Xunit;
using Autofac;
namespace MyClassLibrary.Test
{
    public class AutoFacTestSeven
    {
        interface ILogger
        {
            
        };

        class ConsoleLogger : ILogger
        {
            
        }

        interface IConfigReader
        {
            
        }

        class DemoConfig : IConfigReader
        {
            
        }

        class MyComponet

        {
            string context;
            public MyComponet(ILogger logger, IConfigReader config)
            {
               this.context="ILogger IConfigReader Constructor.";
            }

            public MyComponet()
            {
               this.context="default constructor.";
            }

            public MyComponet(ILogger logger)
            {
                this.context="ILogger Constructor.";
            }

            public string consoleString()
            {
                return context;
            }
        }
        [Fact]
        public void should_console_construct()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MyComponet>();
           // builder.RegisterType<ConsoleLogger>().As<ILogger>();

            IContainer container = builder.Build();
            var result = "default constructor.";
            Assert.Equal(result, container.Resolve<MyComponet>().consoleString());
        }

        public void main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MyComponet>();
           // builder.RegisterType<ConsoleLogger>().As<ILogger>();

            IContainer container = builder.Build();
            container.Resolve<MyComponet>();
        }
             
    }
}