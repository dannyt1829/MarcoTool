using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace MarcoCreatorTool
{
    internal class InputSaver
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static void Save(List<RecordedAction> actions, string filePath)
        {
            string json = JsonSerializer.Serialize(actions, _jsonOptions);
            File.WriteAllText(filePath, json);
        }

        public static List<RecordedAction> Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<RecordedAction>>(json, _jsonOptions) ?? new List<RecordedAction>();
        }
    }
}
