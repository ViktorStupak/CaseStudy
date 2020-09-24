using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace CaseStudy.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31, launchCount: 2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MemoryDiagnoser]
    [MarkdownExporter, HtmlExporter, CsvExporter(CsvSeparator.Semicolon)]
    public class RestFullAsyncTest
    {
        [Benchmark]
        public async Task GetAllProducts()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products");

            var request = new RestRequest(Method.GET);
            await ExecuteAsync(() => client.ExecuteAsync(request));
        }

        [Benchmark]
        public async Task GetAllPaginatedProducts()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products/4/10");
            var request = new RestRequest(Method.GET);
            await ExecuteAsync(() => client.ExecuteAsync(request));
        }

        [Benchmark]
        public async Task GetProduct()
        {
            var client = CreateRestClient("https://localhost:55554/api/Products/55");
            var request = new RestRequest(Method.GET);
            await ExecuteAsync(() => client.ExecuteAsync(request));
        }

        private static RestClient CreateRestClient(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            return client;
        }

        public async Task ExecuteAsync(Func<Task<IRestResponse>> action)
        {
            var result = await action.Invoke().ConfigureAwait(false);
            if (!result.IsSuccessful)
            {
                throw new Exception($"response is not success. status code: {result.StatusCode}");
            }
        }
    }
}
