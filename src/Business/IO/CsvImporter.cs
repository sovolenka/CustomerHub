using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Business.IO;

public class CsvImporter
{
    public static List<T> ImportFromCsv<T>(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        return csv.GetRecords<T>().ToList();
    }
}