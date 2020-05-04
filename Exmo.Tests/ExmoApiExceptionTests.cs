using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace Exmo.Tests
{
    public class ExmoApiExceptionTests
    {
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        [Theory]
        [InlineData("Test Error", 1234)]
        [InlineData("Test Error", null)]
        public void ExmoApiException_Deserialize(string message, int? code)
        {
            var originalException = new ExmoApiException(message) { Code = code };

            using var memoryStream = new MemoryStream();
            _binaryFormatter.Serialize(memoryStream, originalException);
            memoryStream.Position = 0;
            var obj = _binaryFormatter.Deserialize(memoryStream);
            Assert.IsType<ExmoApiException>(obj);

            var actualException = (ExmoApiException)obj;

            Assert.Equal(message, actualException.Message);
            Assert.Equal(code, actualException.Code);
        }
    }
}
