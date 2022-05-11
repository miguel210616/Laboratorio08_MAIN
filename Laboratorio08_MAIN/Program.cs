using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio08_MAIN
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            //IntroToLINQLambda();
            //DataSourceLambda();
            //FilteringLambda();
            //OrderingLambda();
            //DataSource();
            //Filtering();
            //Ordering();
            //GruopingLambda();
            //Gruoping2();
            JoiningLambda();
            //Joining();
            //Gruoping2Lambda();
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;

            foreach(int num in numQuery)
            {
                Console.WriteLine(num);
            }
        }

        static void IntroToLINQLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery2 = numbers.Where(x => x % 2 == 0).ToList();
            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;

            foreach (int num in numQuery2)
            {
                Console.WriteLine(num);
            }
        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach(var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void DataSourceLambda()
        {
            var queryAllCustomers = context.clientes.Select(x => x).ToList();

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad=="Londres"
                                       select cust;

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(x=>x.Ciudad=="Londres").ToList();

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       orderby cust.NombreCompañia ascending
                                       select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes.OrderBy(x => x.NombreCompañia).Where(x => x.Ciudad == "Londres");

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Gruoping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                        group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (cliente customer in customerGroup)
                {
                    Console.WriteLine("     {0}", customer.NombreCompañia);
                }
            }
        }

        static void GruopingLambda()
        {
            var queryCustomersByCity =  context.clientes.GroupBy(x=> x.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (cliente customer in customerGroup)
                {
                    Console.WriteLine("     {0}", customer.NombreCompañia);
                }
            }
        }

        static void Gruoping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Gruoping2Lambda()
        {
            var custQuery = context.clientes.
                GroupBy(x => x.Ciudad).
                Select(x => new { custGroup = x.Count(), x.Key }).
                OrderBy(x => x.Key).Where(x => x.custGroup > 2).ToList();

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario};

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes.Join(context.Pedidos,
                                    cust => cust.idCliente,
                                    dist => dist.IdCliente,
                                    (cust, dist) => new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
