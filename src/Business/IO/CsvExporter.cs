using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Business.IO;

public class CsvExporter
{
    public static void ExportToCsv<T>(IEnumerable<T> data, string filePath)
    {
        var writer = new StreamWriter(filePath);
        var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
        csv.WriteRecords(data);
        writer.Flush();
    }
}