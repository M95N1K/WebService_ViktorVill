using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Console;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Client
{
    class Program
    {
        public class Employee
        {
            public string Name { get; set; }
            public string Firstname { get; set; }
            public string Departament { get; set; }
            public string Position { get; set; }
            public DateTime Birthday { get; set; }
            public int ID { get; set; }

            public override string ToString()
            {
                return $"{ID,8} {Name.Split(' ')[0],10} {Firstname.Split(' ')[0],-10} " +
                    $"{Departament.Split(' ')[0],8} {Position.Split(' ')[0],10} {Birthday.Date.ToString().Split(' ')[0]}";
            }

            public bool IsNull()
            {
                if (ID == -1) return true;
                return false;
            }
        }

        public class Departament
        {
            public string DepartamentName { get; set; }
        }

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            Write("1 - Worker \n2 - Departament \nВыберите: ");
            string answer = ReadLine();
            if (answer[0] == '1')
            {
                var dataList = client.GetStringAsync(@"http://localhost:53573/worker/").Result;

                List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(dataList);
                foreach (var item in list)
                {
                    WriteLine(item.ToString());
                }

                ReadLine();

                Write("Введите Id: ");
                int id = Convert.ToInt32(ReadLine());

                dataList = client.GetStringAsync($@"http://localhost:53573/worker/{id}").Result;

                Employee worker = JsonConvert.DeserializeObject<Employee>(dataList);
                if (!worker.IsNull())
                    WriteLine(worker.ToString());
                else WriteLine($"Записи с ID: {id} не существует");
                ReadLine();


                string sadd = JsonConvert.SerializeObject(new Employee()
                {
                    Name = "QQQ",
                    Firstname = "QQQ",
                    Departament = "depart1",
                    Position = "QQQ",
                    Birthday = DateTime.Today
                });

                StringContent content = new StringContent(
                    content: sadd,
                    encoding: Encoding.UTF8,
                    mediaType: "application/json"
                    );

                var req = client.PostAsync(requestUri: @"http://localhost:53573/addworker", content: content).Result;
                WriteLine(req);
            }
            else if (answer[0] == '2')
            {
                var dataList = client.GetStringAsync($@"http://localhost:53573/departament").Result;

                List<Departament> dList = JsonConvert.DeserializeObject<List<Departament>>(dataList);

                foreach (var item in dList)
                {
                    WriteLine(item.DepartamentName);
                }

                ReadLine();

                string sadd = JsonConvert.SerializeObject(new Departament()
                {
                    DepartamentName = "depart7"
                });

                StringContent content = new StringContent(sadd, Encoding.UTF8, "application/json");
                var req = client.PostAsync(@"http://localhost:53573/adddepart", content).Result;
                WriteLine(req);
            }
            ReadLine();
        }
    }
}
