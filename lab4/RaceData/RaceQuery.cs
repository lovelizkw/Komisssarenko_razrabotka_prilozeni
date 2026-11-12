using System;
using System.Collections.Generic;
using System.Linq;

namespace RaceData
{
    public static class RaceQuery
    {
        public static List<RaceResult> Apply(List<RaceResult> all, string track)
        {
            var query = all.AsEnumerable();

            if (!string.IsNullOrEmpty(track) && track != "Все трассы")
                query = query.Where(r => r.Track == track);

            return query.OrderBy(r => r.Id).ToList();
        }
    }
}
