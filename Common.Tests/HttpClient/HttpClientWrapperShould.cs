using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.HttpClient;
using Moq;
using Moq.Protected;
using DotNetHttpClient = System.Net.Http.HttpClient;

namespace Common.Tests.HttpClient;

public abstract class HttpClientWrapperShould
{
    private readonly Mock<HttpMessageHandler> _handler;
    private const string uri = "http://fakesite.com/api/dosomething";

    private HttpClientWrapper _sut;

    protected HttpClientWrapperShould()
    {
        _handler = new Mock<HttpMessageHandler>();
    }

    public class ExecuteSuccessfully : HttpClientWrapperShould
    {
        [Fact]
        public async Task WhenCallingGetAsync()
        {
            // arrange
            const string testContent = "this worked";
            
            SetupMockHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(testContent)
            });

            var httpClient = new DotNetHttpClient(_handler.Object);

            // act
            _sut = new HttpClientWrapper(httpClient);

            var result = await _sut.GetAsync(uri, CancellationToken.None);
            var resultValue = await result.Content.ReadAsStringAsync();

            // assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(testContent == resultValue);

            _handler.Verify();
        }

        [Fact]
        public async Task WhenCallingPostAsync()
        {
            // arrange
            var content = new StringContent("here's a test content body");
            var body = new TestMessage()
            {
                Message = "here's the response"
            };

            SetupMockHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent(JsonSerializer.Serialize(body))
            });

            var httpClient = new DotNetHttpClient(_handler.Object);

            // act

            _sut = new HttpClientWrapper(httpClient);

            var result = await _sut.PostAsync(uri, content, CancellationToken.None);
            var stream = await result.Content.ReadAsStreamAsync();
            var resultValue = await JsonSerializer.DeserializeAsync<TestMessage>(stream);

            // assert
            Assert.True(result.StatusCode == HttpStatusCode.Created);
            Assert.True(resultValue!.Message == body.Message);

            _handler.Verify();
        }

        [Fact]
        public async Task WhenCallingPutAsync()
        {
            // arrange
            var content = new StringContent("here's a test content");
            var body = new TestMessage
            {
                Message = "ahueauh"
            };

            SetupMockHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(body))
            });

            var httpClient = new DotNetHttpClient(_handler.Object);
            // act
            _sut = new HttpClientWrapper(httpClient);

            var result = await _sut.PutAsync(uri, content, CancellationToken.None);
            var resultBody = await JsonSerializer.DeserializeAsync<TestMessage>(await result!.Content.ReadAsStreamAsync());

            // assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(resultBody!.Message == body.Message);
        }

        [Fact]
        public async Task WhenCallingDeleteAsync()
        {
            // arrange
            var content = new StringContent("delete this");

            SetupMockHandler(new HttpResponseMessage(HttpStatusCode.OK));

            var httpClient = new DotNetHttpClient(_handler.Object);

            // act
            _sut = new HttpClientWrapper(httpClient);

            var result = await _sut.DeleteAsync(uri, CancellationToken.None);

            // assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);
        }


        private void SetupMockHandler(HttpResponseMessage responseMessage)
        {
            _handler.Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(responseMessage)
                    .Verifiable();
        }
    }
    private class TestMessage
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}