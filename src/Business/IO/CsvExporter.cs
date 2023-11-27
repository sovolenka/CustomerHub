using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Business.IO;

public class CsvExporter
{
    public static void ExportToCsv<T>(IEnumerable<T> data, string filePath)
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
        csv.WriteRecords(data);
    }
}