using System.IO;
using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace YmqExample
{
    class Program
    {

        public const string ApiKey = "managed account api key";
        public const string ApiSecret = "managed account api secret";
        public const string QueueUrl = "https://message-queue.api.cloud.yandex.net/xxxxxx/xxxx/my-queue";

        public static async Task Main()
        {
            Amazon.AWSConfigs.EndpointDefinition = Path.Combine(Directory.GetCurrentDirectory(), "yandex.endpoints.json");
            RegionEndpoint endPoint = RegionEndpoint.GetBySystemName("ru-central1");

            var yandexSQSClient = new AmazonSQSClient(ApiKey, ApiSecret, endPoint);

            var sendRequest = new SendMessageRequest();
            sendRequest.QueueUrl = QueueUrl;
            sendRequest.MessageBody = "{ 'message' : 'hello world' }";

            SendMessageResponse smr = await yandexSQSClient.SendMessageAsync(sendRequest);

            Console.WriteLine($"Message id {smr.MessageId} sent with response code {smr.HttpStatusCode} ");
            Console.WriteLine("Press any key to exit");
            await Task.Run(() => Console.ReadKey());

        }
    }
}
