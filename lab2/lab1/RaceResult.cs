using System;
using System.Collections.Generic;
namespace lab1
{
    public class RaceResult
    {
        public long Id { get; set; }
        public string Driver { get; set; } = "";
        public string Car { get; set; } = "";
        public string Track { get; set; } = "";
        public int LapSeconds { get; set; }
    }

    public class RaceRules
    {
        public const int MinLapSeconds = 30;
        public const int MaxLapSeconds = 600;

        public static readonly string[] Cars =
        {
        "Street",
        "Sport",
        "Prototype"
    };

        public static readonly string[] Tracks =
        {
        "Учебный овал",
        "Городское кольцо",
        "Спринт"
    };

        public static void Validate(RaceResult row)
        {
            if (row == null)
                throw new ArgumentException("Запись не может быть пустой");

            string driver = (row.Driver ?? "").Trim();
            if (driver.Length == 0)
                throw new ArgumentException("Имя пилота не может быть пустым");

            if (Array.IndexOf(Cars, row.Car) < 0)
                throw new ArgumentException("Неизвестный автомобиль: " + row.Car);

            if (Array.IndexOf(Tracks, row.Track) < 0)
                throw new ArgumentException("Неизвестная трасса: " + row.Track);

            if (row.LapSeconds < MinLapSeconds || row.LapSeconds > MaxLapSeconds)
                throw new ArgumentException(
                    $"Время должно быть от {MinLapSeconds} до {MaxLapSeconds} секунд");

            row.Driver = driver; 
        }

        public static void ValidateImport(List<RaceResult> rows)
        {
            if (rows == null)
                throw new ArgumentException("Копия не содержит список записей");

            var seenIds = new List<long>();

            foreach (var row in rows)
            {
                Validate(row);

                if (row.Id <= 0)
                    throw new ArgumentException("Id должен быть положительным: " + row.Id);

                if (seenIds.Contains(row.Id))
                    throw new ArgumentException("Повтор Id: " + row.Id);

                seenIds.Add(row.Id);
            }
        }
    }
}
