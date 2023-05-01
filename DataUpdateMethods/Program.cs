using System;
using System.Security.Cryptography;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using DataUpdateMethods.Models;

using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<UpDateData>();
            Console.ReadKey();
        }
    }

    public class UpDateData
    {
        [Benchmark]
        public async Task EfCoreUpdateUpdateRangeAsync()
        {
            var context = new WideWorldImportersContext();
            var invoices = await context.Invoices.ToListAsync();
            invoices.ForEach(i => i.DeliveryInstructions = "Test");
            context.Invoices.UpdateRange(invoices);
        }
    }
}