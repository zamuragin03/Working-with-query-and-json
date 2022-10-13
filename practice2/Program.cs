using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace practice2
{
    class Products
    {
        int ProductID;
        string ProductName;
        decimal UnitPrice;
        bool Discontinued;
        public Products(int productID, string productName, decimal unitPrice, bool discontinued)
        {
            ProductID = productID;
            ProductName = productName;
            UnitPrice = unitPrice;
            Discontinued = discontinued;
        }
        public Products()
        {

        }
        public int ProductID1 { get => ProductID; set => ProductID = value; }
        public string ProductName1 { get => ProductName; set => ProductName = value; }
        public decimal UnitPrice1 { get => UnitPrice; set => UnitPrice = value; }
        public bool Discontinued1 { get => Discontinued; set => Discontinued = value; }
        public override string ToString()
        {
            return String.Format($"{ProductID1,5:d}  {ProductName1,35}  {UnitPrice1,10}  {Discontinued1}");
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwwwww.txt";
            string j1 = File.ReadAllText(path);
            Products[] products0 = (Products[])JsonSerializer.Deserialize(j1, typeof(Products[]));
            foreach (Products t in products0) Console.WriteLine(t);

            IEnumerable<Products> query1 = products0.Where(n => IsFirstLetterA(n.ProductName1));

            Console.WriteLine(new string('—', 10)+ "Названия продуктов с A" + new string('—', 10));
            foreach (var element in query1)
            {
                Console.WriteLine(element);   
            }

            Console.WriteLine(new string('—', 10) + "Поля со скидкой" + new string('—', 10));

            IEnumerable<Products> query2 = products0.Where(n => n.Discontinued1).OrderBy(x => x.UnitPrice1).Take(5);

            foreach (var element in query2)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine(new string('—', 10) + "Продукт с мин ценой" + new string('—', 10));

            IEnumerable<Products> query3 = products0.OrderBy(n=>n.UnitPrice1).Take(1);
            Console.WriteLine(query3.First());

            Console.WriteLine(new string('—', 10) + "Средняя цена скидочных продуктов" + new string('—', 10));
            IEnumerable<Products> query4 = products0.Where(n => n.Discontinued1);
            decimal avgprice = query4.Average(x => x.UnitPrice1);
            Console.WriteLine(avgprice);

            Console.WriteLine(new string('—', 10) + "Кол-во продуктов со скидкой" + new string('—', 10));
            IEnumerable<Products> query5 = products0.Where(n => n.Discontinued1);
            Console.WriteLine(query5.Count());

            Console.WriteLine(new string('—', 10) + "Кол-во продуктов без скидки" + new string('—', 10));
            IEnumerable<Products> query6 = products0.Where(n => !n.Discontinued1);
            Console.WriteLine(query6.Count());

            Console.WriteLine(new string('—', 10) + "Название < 7 символов" + new string('—', 10));
            IEnumerable<Products> query7 = products0.Where(n => IsLowerThan7(n.ProductName1));
            foreach (var element in query7)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine(new string('—', 10) + "Кол-во продуктов со скидкой, цена которых <15" + new string('—', 10));
            IEnumerable<Products> query8 = products0.Where(n=> n.UnitPrice1<15 && n.Discontinued1);

            Console.WriteLine(query8.Count());

            Console.WriteLine(new string('—', 10) + "Кол-во продуктов со скидкой, цена которых >15" + new string('—', 10));
            IEnumerable<Products> query9 = products0.Where(n => n.UnitPrice1 > 15 && n.Discontinued1);

            Console.WriteLine(query9.Count());

            //IEnumerable<Products> numQuery1 = products0.Where(n => n.ProductID1 > 70).Select(n => n);
            //Console.WriteLine("numQuery1");
            //foreach (Products t in numQuery1) Console.WriteLine(t);
            //IEnumerable<string> Query2 =
            //    from num in products0
            //    where num.ProductID1 >= 10 && num.ProductID1 <= 20
            //    select num.ProductID1 + num.ProductName1;
            //foreach (string t in Query2) Console.WriteLine(t);

            bool IsLowerThan7(string ProductName)
            {
                char[] arr = ProductName.ToCharArray();
                if (arr.Length<7)
                {
                    return true;
                }

                return false;
            }

            bool IsFirstLetterA(string ProductName)
            {
                return ProductName.StartsWith('A');
            }
        }
    }
}
