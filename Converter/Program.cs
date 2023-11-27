using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CsvHelper;

namespace Converter
{


    public abstract class DataPrototype
    {
        public abstract DataPrototype Clone();
    }

    public class Data : DataPrototype
    {
        public List<string> Content { get; set; }

        public override DataPrototype Clone()
        {
            return this.MemberwiseClone() as DataPrototype;
        }
    }

    public interface IDataAdapter
    {
        void WriteData(Data data, string filePath);
        Data ReadData(string filePath);
    }

    public class JsonDataAdapter : IDataAdapter
    {
        public void WriteData(Data data, string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(data.Content));
        }

        public Data ReadData(string filePath)
        {
            return new Data { Content = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(filePath)) };
        }
    }

    public class XmlDataAdapter : IDataAdapter
    {
        public void WriteData(Data data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<string>));
            using (var stream = new StreamWriter(filePath))
            {
                serializer.Serialize(stream, data.Content);
            }
        }

        public Data ReadData(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<string>));
            using (var stream = new StreamReader(filePath))
            {
                return new Data { Content = (List<string>)serializer.Deserialize(stream) };
            }
        }
    }

    public class CsvDataAdapter : IDataAdapter
    {
        public void WriteData(Data data, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(data.Content);
            }
        }

        public Data ReadData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader))
            {
                return new Data { Content = new List<string>(csv.GetRecords<string>()) };
            }
        }
    }

    public class Client
    {
        private IDataAdapter _adapter;

        public Client(IDataAdapter adapter)
        {
            _adapter = adapter;
        }

        public void ChangeAdapter(IDataAdapter adapter)
        {
            _adapter = adapter;
        }

        public void SaveData(Data data, string filePath)
        {
            _adapter.WriteData(data, filePath);
        }

        public Data LoadData(string filePath)
        {
            return _adapter.ReadData(filePath);
        }
    }

}
