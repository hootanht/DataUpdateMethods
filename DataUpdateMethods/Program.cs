using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DataUpdateMethods.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataUpdateMethods
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            BenchmarkRunner.Run<UpDateData>();
            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddDbContext<WideWorldImportersContext>()
                            .AddTransient<UpDateData>()
                            .AddTransient<BenchmarkRunner>());
    }

    [RPlotExporter]
    public class UpDateData
    {
        private readonly WideWorldImportersContext _context;

        public UpDateData(WideWorldImportersContext context)
        {
            _context = context;
        }

        [Benchmark]
        public async Task EfCoreUpdateRangeAsync()
        {
            var people = await _context.People.ToListAsync();
            people.ForEach(i => i.LogonName = "Test");
            _context.People.UpdateRange(people);
            await _context.SaveChangesAsync();
        }

        [Benchmark]
        public async Task EfCoreExecuteUpdateAsync()
        {
            await _context.People.ExecuteUpdateAsync(p => p.SetProperty(pr => pr.LogonName, "Test"));
            await _context.SaveChangesAsync();
        }

        [Benchmark]
        public async Task EfCoreBatchUpdateAsync()
        {
            await _context.Set<Person>()
                .BatchUpdateAsync(new Person()
                {
                    LogonName = "Test"
                }, new List<string>()
                {
                    nameof(Person.LogonName)
                }, CancellationToken.None);
        }

        [Benchmark]
        public void EfCoreBatchUpdate()
        {
            _context.Set<Person>()
               .BatchUpdate(new Person()
               {
                   LogonName = "Test"
               }, new List<string>()
               {
                    nameof(Person.LogonName)
               });
        }

        [Benchmark]
        public async Task EfCoreBulkUpdateAsync()
        {
            var people = _context.People.ToList();
            people.ForEach(p => p.LogonName = "Test");
            await _context.BulkUpdateAsync(people);
        }
    }
}
