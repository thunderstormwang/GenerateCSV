using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCSV
{
    public class Example01 : IExample
    {
        public void Start()
        {
            List<Address> addressList = new List<Address>();
            addressList.Add(new Address() { Name = "name123", Phone = "12345678", Date = "20140101" });
            addressList.Add(new Address() { Name = "name456", Phone = "12341234", Date = "20140201" });
            addressList.Add(new Address() { Name = "name789", Phone = "1234", Date = "20140301" });

            GenerateCSV<Address>(addressList);
        }

        private void GenerateCSV<T>(IEnumerable<T> list)
        {
            // 未去掉最後一個逗號

            Type t = typeof(T);

            //將資料寫到前端
            using(StreamWriter sw = new StreamWriter(@"C:\AA.csv", true))
            {            

            object o = Activator.CreateInstance(t);

            PropertyInfo[] props = o.GetType().GetProperties();

            //將資料表的欄位名稱寫出
            foreach (PropertyInfo pi in props)
            {
                sw.Write(pi.Name.ToUpper() + ",");
            }
            sw.WriteLine();

            //將資料表的資料寫出
            foreach (T item in list)
            {
                foreach (PropertyInfo pi in props)
                {
                    string whatToWrite = Convert.ToString(item.GetType().GetProperty(pi.Name).GetValue(item, null)).Replace(',', ' ') + ',';
                    sw.Write(whatToWrite);
                }
                sw.WriteLine();
            }
            }
        } 
    }
}
