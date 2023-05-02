using System;
using System.Security.Cryptography;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using DataUpdateMethods.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;


namespace DataUpdateMethods
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<UpDateData>();

            //var p = new UpDateData();
            //await p.EfCoreBlukUpdateAsync();
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

        //[Benchmark]
        //public async Task EfCoreExecuteUpdateAsync()
        //{
        //    var context = new WideWorldImportersContext();
        //    await context.People.ExecuteUpdateAsync(p => p.SetProperty(pr => pr.LogonName, "Test"));
        //    await context.SaveChangesAsync();
        //}

        //[Benchmark]
        //public async Task EfCoreBatchUpdateAsync()
        //{
        //    var context = new WideWorldImportersContext();
        //    await context.Set<Person>()
        //        .BatchUpdateAsync(new Person()
        //        {
        //            LogonName = "Test"
        //        }, new List<string>()
        //        {
        //            nameof(Person.LogonName)
        //        }, CancellationToken.None);
        //}

        //[Benchmark]
        //public void EfCoreBatchUpdate()
        //{
        //    var context = new WideWorldImportersContext();
        //    context.Set<Person>()
        //       .BatchUpdate(new Person()
        //       {
        //           LogonName = "Test"
        //       }, new List<string>()
        //       {
        //            nameof(Person.LogonName)
        //       });
        //}

        [Benchmark]
        public async Task EfCoreBlukUpdateAsync()
        {
            var context = new WideWorldImportersContext();
            var people = context.People.ToList();
            people.ForEach(p => p.LogonName = "Test");
            await context.BulkUpdateAsync(people);
        }
    }
}