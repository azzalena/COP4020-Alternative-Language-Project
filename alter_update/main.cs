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
  // Method to calculate average body weight for a given list of cells
  public static Dictionary<string, float> CalculateAverageBodyWeightByOEM(List<Cell> cells)
  {
      Dictionary<string, List<float>> weightByOEM = new Dictionary<string, List<float>>();

      foreach (var cell in cells)
      {
          if (!weightByOEM.ContainsKey(cell.OEM))
          {
              weightByOEM[cell.OEM] = new List<float>();
          }
          weightByOEM[cell.OEM].Add(cell.BodyWeight);
      }

      Dictionary<string, float> averageWeightByOEM = new Dictionary<string, float>();
      foreach (var kvp in weightByOEM)
      {
          averageWeightByOEM[kvp.Key] = kvp.Value.Average();
      }

      return averageWeightByOEM;
  }

  // Method to find phones announced in one year and released in another
  public static List<Cell> FindPhonesAnnouncedReleasedDifferentYears(List<Cell> cells)
  {
      return cells.Where(c => c.LaunchAnnounced != 0 && c.LaunchReleased != 0 && c.LaunchAnnounced != c.LaunchReleased).ToList();
  }

  // Method to count phones with only one feature sensor
  public static int CountPhonesWithOneFeatureSensor(List<Cell> cells)
  {
      return cells.Count(c => c.FeaturesSensors.Split(',').Length == 1);
  }

  // Method to find the year with the most phones launched
  public static int FindYearWithMostPhonesLaunched(List<Cell> cells)
  {
      return cells.Where(c => c.LaunchAnnounced > 1999)
                  .GroupBy(c => c.LaunchAnnounced)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g.Key)
                  .FirstOrDefault();
  }
    // ToString method
    public override string ToString()
    {
        return $"OEM: {OEM}, Model: {Model}, Launch Announced: {LaunchAnnounced}, " +
               $"Launch Released: {LaunchReleased}, Body Dimensions: {BodyDimensions}, " +
               $"Body Weight: {BodyWeight}, Body Sim: {BodySim}, Display Type: {DisplayType}, " +
               $"Display Size: {DisplaySize}, Display Resolution: {DisplayResolution}, " +
               $"Features Sensors: {FeaturesSensors}, Platform OS: {PlatformOS}";
    }

    // Method to calculate mean of body weight
    public static float CalculateMeanBodyWeight(List<Cell> cells)
    {
        return cells.Select(c => c.BodyWeight).Average();
    }

    // Method to calculate median of body weight
    public static float CalculateMedianBodyWeight(List<Cell> cells)
    {
        var sortedWeights = cells.Select(c => c.BodyWeight).OrderBy(w => w).ToList();
        int count = sortedWeights.Count;
        if (count % 2 == 0)
        {
            return (sortedWeights[count / 2 - 1] + sortedWeights[count / 2]) / 2;
        }
        else
        {
            return sortedWeights[count / 2];
        }
    }

    // Method to find unique values in a specified column
    public static List<string> FindUniqueValues(List<Cell> cells, string columnName)
    {
        switch (columnName)
        {
            case "OEM":
                return cells.Select(c => c.OEM).Distinct().ToList();
            case "Model":
                return cells.Select(c => c.Model).Distinct().ToList();
            case "LaunchAnnounced":
                return cells.Select(c => c.LaunchAnnounced.ToString()).Distinct().ToList();
            case "LaunchReleased":
                return cells.Select(c => c.LaunchReleased.ToString()).Distinct().ToList();
            case "BodyDimensions":
                return cells.Select(c => c.BodyDimensions).Distinct().ToList();
            case "BodySim":
                return cells.Select(c => c.BodySim).Distinct().ToList();
            case "DisplayType":
                return cells.Select(c => c.DisplayType).Distinct().ToList();
            case "DisplayResolution":
                return cells.Select(c => c.DisplayResolution).Distinct().ToList();
            case "FeaturesSensors":
                return cells.Select(c => c.FeaturesSensors).Distinct().ToList();
            case "PlatformOS":
                return cells.Select(c => c.PlatformOS).Distinct().ToList();
            default:
                return new List<string>();
        }
    }

    // Method to add a new cell
    public static void AddCell(List<Cell> cells, Cell newCell)
    {
        cells.Add(newCell);
    }

    // Method to delete a cell by index
    public static void DeleteCellByIndex(List<Cell> cells, int index)
    {
        if (index >= 0 && index < cells.Count)
        {
            cells.RemoveAt(index);
        }
    }

    // Method to calculate descriptive statistics for a numeric column
    public static (float Mean, float Median, float StandardDeviation) CalculateNumericStats(List<float> values)
    {
        float mean = values.Average();
        float median;
        float standardDeviation;

        if (values.Count == 0)
        {
            median = 0;
            standardDeviation = 0;
        }
        else
        {
            var sortedValues = values.OrderBy(v => v).ToList();
            int count = sortedValues.Count;
            if (count % 2 == 0)
            {
                median = (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2;
            }
            else
            {
                median = sortedValues[count / 2];
            }

            float sumOfSquaresOfDifferences = values.Select(val => (val - mean) * (val - mean)).Sum();
            standardDeviation = (float)Math.Sqrt(sumOfSquaresOfDifferences / values.Count);
        }

        return (mean, median, standardDeviation);
    }

    // Method to calculate mode for a categorical column
    public static string CalculateMode(List<string> values)
    {
        var groups = values.GroupBy(v => v);
        var maxCount = groups.Max(g => g.Count());
        var mode = groups.First(g => g.Count() == maxCount).Key;
        return mode;
    }

    // Method to count unique values for each column
    public static Dictionary<string, int> CountUniqueValues(List<Cell> cells)
    {
        Dictionary<string, int> uniqueValueCounts = new Dictionary<string, int>();
        uniqueValueCounts["OEM"] = cells.Select(c => c.OEM).Distinct().Count();
        uniqueValueCounts["Model"] = cells.Select(c => c.Model).Distinct().Count();
        uniqueValueCounts["LaunchAnnounced"] = cells.Select(c => c.LaunchAnnounced.ToString()).Distinct().Count();
        uniqueValueCounts["LaunchReleased"] = cells.Select(c => c.LaunchReleased.ToString()).Distinct().Count();
        uniqueValueCounts["BodyDimensions"] = cells.Select(c => c.BodyDimensions).Distinct().Count();
        uniqueValueCounts["BodySim"] = cells.Select(c => c.BodySim).Distinct().Count();
        uniqueValueCounts["DisplayType"] = cells.Select(c => c.DisplayType).Distinct().Count();
        uniqueValueCounts["DisplayResolution"] = cells.Select(c => c.DisplayResolution).Distinct().Count();
        uniqueValueCounts["FeaturesSensors"] = cells.Select(c => c.FeaturesSensors).Distinct().Count();
        uniqueValueCounts["PlatformOS"] = cells.Select(c => c.PlatformOS).Distinct().Count();
        return uniqueValueCounts;
    }

    // Method to load and clean data from cells.csv file
  public static List<Cell> LoadAndCleanData(string filePath)
  {
      List<Cell> cells = new List<Cell>();

      if (!File.Exists(filePath))
      {
          Console.WriteLine("File not found.");
          return cells;
      }

      using (StreamReader reader = new StreamReader(filePath))
      {
          string line;
          while ((line = reader.ReadLine()) != null)
          {
              List<string> rowData = line.Split(',').ToList();

              // Clean the data
              List<string> cleanedData = CleanData(rowData);

              // Convert string data to appropriate types
              string oem = cleanedData[0];
              string model = cleanedData[1];
              int launchAnnounced;
              if (!int.TryParse(cleanedData[2], out launchAnnounced))
              {
                  Console.WriteLine($"Error converting launch announced year: {cleanedData[2]}. Setting to 0.");
                  launchAnnounced = 0;
              }
              int launchReleased;
              if (!int.TryParse(cleanedData[3], out launchReleased))
              {
                  Console.WriteLine($"Error converting launch released year: {cleanedData[3]}. Setting to 0.");
                  launchReleased = 0;
              }
              string bodyDimensions = cleanedData[4];
              float bodyWeight;
              if (!float.TryParse(cleanedData[5], out bodyWeight))
              {
                  Console.WriteLine($"Error converting body weight: {cleanedData[5]}. Setting to 0.");
                  bodyWeight = 0;
              }
              string bodySim = cleanedData[6];
              string displayType = cleanedData[7];
              float displaySize;
              if (!float.TryParse(cleanedData[8], out displaySize))
              {
                  Console.WriteLine($"Error converting display size: {cleanedData[8]}. Setting to 0.");
                  displaySize = 0;
              }
              string displayResolution = cleanedData[9];
              string featuresSensors = cleanedData[10];
              string platformOS = cleanedData[11];

              // Create and add new Cell object to the list
              cells.Add(new Cell(oem, model, launchAnnounced, launchReleased,
                                  bodyDimensions, bodyWeight, bodySim, displayType,
                                  displaySize, displayResolution, featuresSensors, platformOS));
          }
      }

      return cells;
  }

    // Method to clean data
    private static List<string> CleanData(List<string> rowData)
    {
        List<string> cleanedRow = new List<string>();
        for (int i = 0; i < rowData.Count; i++)
        {
            string cleanedValue = "";
            switch (i)
            {
                case 0: // oem
                case 1: // model
                case 4: // body_dimensions
                case 6: // display_type
                case 8: // display_resolution
                case 9: // features_sensors
                    cleanedValue = ReplaceMissingWithNull(rowData[i]);
                    break;
                case 2: // launch_announced
                case 3: // launch_status
                    cleanedValue = (i == 2) ? ExtractYear(rowData[i]) : ReplaceMissingWithNull(rowData[i]);
                    break;
                case 5: // body_weight
                    cleanedValue = ExtractWeight(rowData[i]);
                    if (!IsValidFloat(cleanedValue))
                    {
                        cleanedValue = "null";
                    }
                    break;
                case 7: // body_sim
                    cleanedValue = (rowData[i] == "No" || rowData[i] == "Yes") ? "null" : rowData[i];
                    break;
                case 10: // platform_os
                    Regex osRegex = new Regex("^[^,]+");
                    Match match = osRegex.Match(rowData[i]);
                    cleanedValue = match.Success ? match.Value : "null";
                    break;
                case 11: // display_size
                    Regex sizeRegex = new Regex(@"\b\d+(\.\d+)?\s*inches\b");
                    match = sizeRegex.Match(rowData[i]);
                    cleanedValue = match.Success ? match.Value : "null";
                    break;
            }
            cleanedRow.Add(cleanedValue);
        }
        return cleanedRow;
    }

    // Function to replace missing or "-" values with null
    private static string ReplaceMissingWithNull(string value)
    {
        if (value == "-" || string.IsNullOrEmpty(value))
        {
            return "null";
        }
        return value;
    }

    // Function to transform string to integer year
    private static string ExtractYear(string value)
    {
        Regex yearRegex = new Regex(@"\b\d{4}\b");
        Match match = yearRegex.Match(value);
        return match.Success ? match.Value : "null";
    }

    // Function to transform string to float weight in grams
    private static string ExtractWeight(string value)
    {
        Regex weightRegex = new Regex(@"\b\d+\b");
        Match match = weightRegex.Match(value);
        return match.Success ? match.Value : "null";
    }

    // Function to check if string is a valid float
    private static bool IsValidFloat(string s)
    {
        return !string.IsNullOrEmpty(s) && s.All(char.IsDigit) || s.Count(c => c == '.') == 1 && s.Split('.').All(p => p.All(char.IsDigit));
    }
  public static List<Cell> PhonesAnnouncedAndReleasedInDifferentYears(List<Cell> cells)
  {
      return cells.Where(c => c.LaunchAnnounced != c.LaunchReleased).ToList();
  }
}

class Program
{
    static void Main(string[] args)
    {
        // Load and clean data from cells.csv
        List<Cell> cells = Cell.LoadAndCleanData("cells.csv");

        // Sample usage of Cell class methods with loaded data
        if (cells.Count > 0)
        {
            // Calculate average body weight by OEM
            var averageWeightByOEM = Cell.CalculateAverageBodyWeightByOEM(cells);
            var maxAverageWeight = averageWeightByOEM.Values.Max();
            var oemWithMaxAverageWeight = averageWeightByOEM.FirstOrDefault(x => x.Value == maxAverageWeight).Key;
            Console.WriteLine($"Company with the highest average weight of the phone body: {oemWithMaxAverageWeight}");
            foreach (var cell in cells)
            {
                Console.WriteLine(cell);
            }
            // Find phones announced in one year and released in another
          List<Cell> phonesAnnouncedAndReleasedInDifferentYears = Cell.PhonesAnnouncedAndReleasedInDifferentYears(cells);
          Console.WriteLine("\nPhones announced in one year and released in another:");
          foreach (var phone in phonesAnnouncedAndReleasedInDifferentYears)
          {
              Console.WriteLine($"{phone.OEM} - {phone.Model}");
          }

            // Count phones with only one feature sensor
            var countPhonesWithOneFeatureSensor = Cell.CountPhonesWithOneFeatureSensor(cells);
            Console.WriteLine($"\nNumber of phones with only one feature sensor: {countPhonesWithOneFeatureSensor}");

            // Find the year with the most phones launched after 1999
            var yearWithMostPhonesLaunched = Cell.FindYearWithMostPhonesLaunched(cells);
            Console.WriteLine($"\nYear with the most phones launched after 1999: {yearWithMostPhonesLaunched}");
        }
        else
        {
            Console.WriteLine("No data loaded.");
        }
    }
}
