using System;
using System.Collections.Generic;

namespace RaceData
{
    public static class RaceRules
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
    }
}
