using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductMonitorWinForms
{
    public enum FilterPeriod
    {
        Today,
        ThisMonth,
        ThisYear
    }

    public class ProductRecord
    {
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class DataManager
    {
        private readonly List<ProductRecord> _records = new();

        public void AddQuery(string name)
        {
            _records.Add(new ProductRecord { Name = name, Timestamp = DateTime.Now });
        }

        public Dictionary<string, int> GetCounts(FilterPeriod period)
        {
            DateTime start = period switch
            {
                FilterPeriod.Today => DateTime.Today,
                FilterPeriod.ThisMonth => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                FilterPeriod.ThisYear => new DateTime(DateTime.Today.Year, 1, 1),
                _ => DateTime.MinValue
            };

            return _records
                .Where(r => r.Timestamp >= start)
                .GroupBy(r => r.Name)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
