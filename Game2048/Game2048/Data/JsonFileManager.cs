using Game2048.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Game2048.Data
{
    public class JsonFileManager
    {
        private const string DefaultErrorMessage = "Помилка під час читання статистики з файлу! Буде виконано скидання!";
        public static void WriteToJsonFile<T>(string filePath, ObservableCollection<T> players)
        {
            var jsonString = SerializeToJson(players);
            SaveToFile(filePath, jsonString);
        }
        private static string SerializeToJson<T>(ObservableCollection<T> players)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(players, options);
        }
        private static void SaveToFile(string filePath, string jsonString)
        {
            File.WriteAllText(filePath, jsonString);
        }
        private static void CreateFile(string filePath)
        {
            File.Create(filePath).Close();
        }
        public static ObservableCollection<T> ReadListFromJsonFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateFile(filePath);
                return new ObservableCollection<T>();
            }
            return DeserializeFromJson<T>(filePath);
        }

        private static ObservableCollection<T> DeserializeFromJson<T>(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);

            try
            {
                return JsonSerializer.Deserialize<ObservableCollection<T>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }   
            catch (JsonException)
            {
                HandleDeserializationError(filePath);
                return new ObservableCollection<T>();
            }
        }
        private static void HandleDeserializationError(string filePath)
        {
            ShowErrorMessage();
            ClearFileContent(filePath);
        }
        private static void ClearFileContent(string filePath)
        {
            File.WriteAllText(filePath, string.Empty);
        }
        private static void ShowErrorMessage()
        {
            MessageBox.Show(DefaultErrorMessage, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
