using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using RestSharp;
using System;

namespace CaseStudy.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31, launchCount: 2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MemoryDiagnoser]
    [MarkdownExporter, HtmlExporter, CsvExporter(CsvSeparator.Semicolon)]
    public class RestFullTest
    {
        [Benchmark]
        public void GetAllProducts()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products");
            var request = new RestRequest(Method.GET);
            Execute(() => client.Execute(request));
        }

        [Benchmark]
        public void GetAllPaginatedProducts()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products/4/10");
            var request = new RestRequest(Method.GET);
            Execute(() => client.Execute(request));
        }

        [Benchmark]
        public void GetProduct()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products/55");
            var request = new RestRequest(Method.GET);
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
