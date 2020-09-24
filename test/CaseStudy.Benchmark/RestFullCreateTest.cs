using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using RestSharp;
using System;

namespace CaseStudy.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31, launchCount: 1, 5, 10)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MemoryDiagnoser]
    [MarkdownExporter, HtmlExporter, CsvExporter(CsvSeparator.Semicolon)]
    public class RestFullCreateTest
    {
        [Benchmark]
        public void PostProduct()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json",
                "{\r\n  \"name\": \"Men's basketball shoes\",\r\n  \"imgUri\": \"http\\\\\\\\test.com\",\r\n  \"price\": 10,\r\n  \"description\": \"Description of the product\"\r\n}",
                ParameterType.RequestBody);
            Execute(() => client.Execute(request));
        }
        [Benchmark]
        public void PutProduct()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"id\": 58,\r\n  \"name\": \"Men's basketball shoes\",\r\n  \"imgUri\": \"http\\\\\\\\test.com\",\r\n  \"price\": 10,\r\n  \"description\": \"Description of the product\"\r\n}", ParameterType.RequestBody);
            Execute(() => client.Execute(request));
        }

        [Benchmark]
        public void PutProductDescription()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products/description/55");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"description\": \"Description of the product\"\r\n}", ParameterType.RequestBody);
            Execute(() => client.Execute(request));
        }

        private static RestClient CreateRestClient(string url)
        {
            var client = new RestClient(url)
            {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };
            return client;
        }

        private static void Execute(Func<IRestResponse> action)
        {
            var result = action.Invoke();
            if (!result.IsSuccessful)
            {
                throw new Exception($"response is not success. status code: {result.StatusCode}");
            }
        }
    }
}
