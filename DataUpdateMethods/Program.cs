using DataUpdateMethods.Models;

namespace DataUpdateMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new WideWorldImportersContext();
            var customer = context.Customers.Find(1);
            Console.WriteLine(customer?.CustomerName);
            Console.ReadKey();
        }
    }
}