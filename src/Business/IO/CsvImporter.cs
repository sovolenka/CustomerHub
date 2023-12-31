﻿using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;


namespace Business.IO;

public class CsvImporter
{
    public static IEnumerable<T> ImportFromCsv<T>(string filePath)
    {
        var reader = new StreamReader(filePath);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        return csv.GetRecords<T>();
    }
}