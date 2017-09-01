using System;
using Autofac;
using Xunit;
namespace MyClassLibrary.Test
{
    public class AutoFacTest
    {
        interface Animal
        {
        };

        class Duck : Animal
        {
            
        }

        class Goat : Animal
        {

        };


        interface Speak
        {
            string SpeakName(string name);
        };

        class DogSpeak : Speak
        {
           public string SpeakName(string name)
           {
               return "I am dog,my name is "+name;
           }
        }

        class DuckSpeak : Speak
        {
            public string SpeakName(string name)
            {
                return "I am duck,my name is "+name;
            }
        }

        class CallSpeak
        {
            Speak speak;

            public CallSpeak(Speak speak)
            {
                this.speak = speak;
            }

            public string SpeakName(string name)
            {
                return speak.SpeakName(name);
            }
        }


        [Fact]
        public void should_creat()
        {
            var builder = new ContainerBuilder();
            builder.Register(_=>new Duck()).As<Animal>();

            IContainer container = builder.Build();

            Assert.Equal(typeof(Duck),container.Resolve<Animal>().GetType());

        }

        [Fact]
        public void should_get_dogname()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new DogSpeak()).As<Speak>();
            builder.Register(c => new CallSpeak(c.Resolve<Speak>()));

            IContainer container=builder.Build();
            string result = "I am dog,my name is maomao";
            Assert.Equal(result, container.Resolve<CallSpeak>().SpeakName("maomao"));


        }

        [Fact]
        public void should_get_duckspeak()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new DuckSpeak()).As<Speak>();
            builder.Register(_ => new CallSpeak(_.Resolve<Speak>()));

            IContainer container = builder.Build();
            string result = "I am duck,my name is maomao";
            Assert.Equal(result, container.Resolve<CallSpeak>().SpeakName("maomao"));
        }

    }
}