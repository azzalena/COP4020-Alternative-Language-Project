using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Cell
{
    // Properties
    public string OEM { get; set; }
    public string Model { get; set; }
    public int LaunchAnnounced { get; set; }
    public int LaunchReleased { get; set; }
    public string BodyDimensions { get; set; }
    public float BodyWeight { get; set; }
    public string BodySim { get; set; }
    public string DisplayType { get; set; }
    public float DisplaySize { get; set; }
    public string DisplayResolution { get; set; }
    public string FeaturesSensors { get; set; }
    public string PlatformOS { get; set; }

    // Constructor
    public Cell(string oem, string model, int launchAnnounced, int launchReleased,
                string bodyDimensions, float bodyWeight, string bodySim, string displayType,
                float displaySize, string displayResolution, string featuresSensors, string platformOS)
    {
        OEM = oem;
        Model = model;
        LaunchAnnounced = launchAnnounced;
        LaunchReleased = launchReleased;
        BodyDimensions = bodyDimensions;
        BodyWeight = bodyWeight;
        BodySim = bodySim;
        DisplayType = displayType;
        DisplaySize = displaySize;
        DisplayResolution = displayResolution;
        FeaturesSensors = featuresSensors;
        PlatformOS = platformOS;
    }

    // Method to replace missing or "-" values with null
    private static string ReplaceMissingWithNull(string value)
    {
        if (value == "-" || string.IsNullOrEmpty(value))
        {
            return "null";
        }
        return value;
    }

    // Method to transform string to integer year
    private static int ExtractYear(string value)
    {
        Regex yearRegex = new Regex(@"\b\d{4}\b");
        Match match = yearRegex.Match(value);
        return match.Success ? int.Parse(match.Value) : 0;
    }

    // Method to transform string to float weight in grams
    private static float ExtractWeight(string value)
    {
        Regex weightRegex = new Regex(@"\b\d+\b");
        Match match = weightRegex.Match(value);
        return match.Success ? float.Parse(match.Value) : 0.0f;
    }

    // Method to clean and transform data from CSV
    public static List<Cell> CleanCsvData(string filePath)
    {
        List<Cell> cleanedData = new List<Cell>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return cleanedData;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                List<string> rowData = line.Split(',').ToList();

                // Clean the data
                string oem = ReplaceMissingWithNull(rowData[0]);
                string model = ReplaceMissingWithNull(rowData[1]);
                int launchAnnounced = ExtractYear(rowData[2]);
                int launchReleased = ExtractYear(rowData[3]);
                string bodyDimensions = ReplaceMissingWithNull(rowData[4]);
                float bodyWeight = ExtractWeight(rowData[5]);
                string bodySim = ReplaceMissingWithNull(rowData[6]);
                string displayType = ReplaceMissingWithNull(rowData[7]);
                float displaySize = ExtractWeight(rowData[8]);
                string displayResolution = ReplaceMissingWithNull(rowData[9]);
                string featuresSensors = ReplaceMissingWithNull(rowData[10]);
                string platformOS = ReplaceMissingWithNull(rowData[11]);

                // Create Cell object and add to cleaned data list
                Cell cell = new Cell(oem, model, launchAnnounced, launchReleased,
                                     bodyDimensions, bodyWeight, bodySim, displayType,
                                     displaySize, displayResolution, featuresSensors, platformOS);
                cleanedData.Add(cell);
            }
        }

        return cleanedData;
    }

    // Additional methods as described in the prompt...

    // Method to calculate mean of body weight
    public static string HighestAverageBodyWeight(List<Cell> cells)
    {
        var avgBodyWeightByOEM = cells.GroupBy(c => c.OEM)
                                      .Select(g => new { OEM = g.Key, AvgWeight = g.Average(c => c.BodyWeight) })
                                      .OrderByDescending(x => x.AvgWeight)
                                      .FirstOrDefault();
        return avgBodyWeightByOEM != null ? avgBodyWeightByOEM.OEM : "N/A";
    }

    // Method to find phones announced in one year and released in another
    public static List<Cell> PhonesAnnouncedAndReleasedInDifferentYears(List<Cell> cells)
    {
        return cells.Where(c => c.LaunchAnnounced != c.LaunchReleased).ToList();
    }

    // Method to count phones with only one feature sensor
    public static int PhonesWithOneFeatureSensor(List<Cell> cells)
    {
        return cells.Count(c => c.FeaturesSensors != "null" && c.FeaturesSensors.Split(',').Length == 1);
    }

    // Method to find the year with the most phone launches after 1999
    public static int YearWithMostLaunchesAfter1999(List<Cell> cells)
    {
        return cells.Where(c => c.LaunchAnnounced > 1999)
                    .GroupBy(c => c.LaunchAnnounced)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "cells.csv";

        // Clean the CSV data
        List<Cell> cells = Cell.CleanCsvData(filePath);

        // Query 1: Company with the highest average weight of the phone body
        string companyWithHighestAvgWeight = Cell.HighestAverageBodyWeight(cells);
        Console.WriteLine($"Company with the highest average weight of the phone body: {companyWithHighestAvgWeight}");

        // Query 2: Phones announced in one year and released in another
        List<Cell> phonesAnnouncedAndReleasedInDifferentYears = Cell.PhonesAnnouncedAndReleasedInDifferentYears(cells);
        Console.WriteLine("\nPhones announced in one year and released in another:");
        foreach (var phone in phonesAnnouncedAndReleasedInDifferentYears)
        {
            Console.WriteLine($"{phone.OEM} - {phone.Model}");
        }

        // Query 3: Number of phones with only one feature sensor
        int phonesWithOneFeatureSensor = Cell.PhonesWithOneFeatureSensor(cells);
        Console.WriteLine($"\nNumber of phones with only one feature sensor: {phonesWithOneFeatureSensor}");

        // Query 4: Year with the most phone launches after 1999
        int yearWithMostLaunches = Cell.YearWithMostLaunchesAfter1999(cells);
        Console.WriteLine($"\nYear with the most phone launches after 1999: {yearWithMostLaunches}");
    }
}
