using BenchmarkDotNet.Running;

namespace CaseStudy.Benchmark
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //BenchmarkRunner.Run<RepositoryTests>();
            //BenchmarkRunner.Run<RepositoryLargeTests>();
            BenchmarkRunner.Run<RestFullAsyncTest>();
            BenchmarkRunner.Run<RestFullTest>();
            BenchmarkRunner.Run<RestFullCreateTest>();
            BenchmarkRunner.Run<RestFullCreateAsyncTest>();
        }
    }
}
