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

    [RPlotExporter]
    public class UpDateData
    {
        //[Benchmark]
        //public async Task EfCoreUpdateRangeAsync()
        //{
        //    var context = new WideWorldImportersContext();
        //    var people = await context.People.ToListAsync();
        //    people.ForEach(i => i.LogonName = "Test");
        //    context.People.UpdateRange(people);
        //    await context.SaveChangesAsync();
        //}

        [Benchmark]
        public async Task EfCoreExecuteUpdateAsync()
        {
            var context = new WideWorldImportersContext();
            await context.People.ExecuteUpdateAsync(p => p.SetProperty(pr => pr.LogonName, "Test"));
            await context.SaveChangesAsync();
        }
    }
}