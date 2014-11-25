using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCSV
{
    public class Example02 : IExample
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
            // 最後一欄後面有沒逗號
            string delimiter = ",";

            Type t = typeof(T);

            //將資料寫到前端
            using (StreamWriter sw = new StreamWriter(@"C:\AA.csv", true))
            {
                object o = Activator.CreateInstance(t);

                PropertyInfo[] props = o.GetType().GetProperties();

                //將資料表的欄位名稱寫出
                List<string> propertyNameList = new List<string>();
                foreach (PropertyInfo pi in props)
                {
                    propertyNameList.Add(pi.Name.ToUpper());
                }
                string header = string.Join(delimiter, propertyNameList);
                sw.Write(header);
                sw.WriteLine();

                //將資料表的資料寫出
                foreach (T item in list)
                {
                    List<string> propertyValueList = new List<string>();

                    foreach (PropertyInfo pi in props)
                    {
                        propertyValueList.Add(Convert.ToString(item.GetType().GetProperty(pi.Name).GetValue(item, null)).Replace(',', ' '));
                    }
                    string value = string.Join(delimiter, propertyValueList);
                    sw.Write(value);
                    sw.WriteLine();
                }
            }
        }
    }
}
