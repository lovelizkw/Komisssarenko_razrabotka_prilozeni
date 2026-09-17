using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace lab1
{
    public static class RaceTransfer
    {
        public static void SaveJson(string path, List<RaceResult> rows)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rows, options);
            File.WriteAllText(path, json);
        }

        public static List<RaceResult> LoadJson(string path)
        {
            string json = File.ReadAllText(path);
            var rows = JsonSerializer.Deserialize<List<RaceResult>>(json);
            if (rows == null)
                throw new System.Exception("Файл JSON пуст или повреждён");
            return rows;
        }

        public static void SaveXml(string path, List<RaceResult> rows)
        {
            var serializer = new XmlSerializer(typeof(List<RaceResult>));
            using var stream = File.Create(path);
            serializer.Serialize(stream, rows);
        }

        public static List<RaceResult> LoadXml(string path)
        {
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null
            };

            using var reader = XmlReader.Create(path, settings);
            var serializer = new XmlSerializer(typeof(List<RaceResult>));
            var rows = serializer.Deserialize(reader) as List<RaceResult>;
            if (rows == null)
                throw new System.Exception("Файл XML пуст или повреждён");
            return rows;
        }
    }
}