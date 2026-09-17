using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1
{
    public static class RaceQuery
    {
        public static List<RaceResult> Apply(List<RaceResult> all, string text, string track)
        {
            string search = (text ?? "").Trim();
            var query = all.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(r =>
                    r.Driver.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

            if (!string.IsNullOrEmpty(track) && track != "Все трассы")
                query = query.Where(r => r.Track == track);

            return query.OrderBy(r => r.Id).ToList();
        }
    }
}