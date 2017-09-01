using Xunit;
using MyClassLibrary.Duck;

namespace MyClassLibrary.Test
{
    public class AnimalFact
    {
        [Fact]
        public void should_return_string()
        {
            Animal animal = new Animal();
            var resultSpeak = "I am an animal.";
            Assert.Equal(resultSpeak,animal.speak());
        }
        [Fact]
        public void should_return_string_duck()
        {
            Duck.Duck duck = new Duck.Duck();
            string result = "I am a duck.";
            Assert.Equal(result,duck.speak());
        }
    }
}