using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;

public class LoadingData {


    private static DateTime lastModifiedTime = DateTime.MinValue;
    private static Dictionary<int,SuburbRecord>? cachedData = null;

    public virtual Dictionary<int,SuburbRecord> loadSuburbData(string filepath)
    {

        var file = new FileInfo(filepath);

        if(file.LastWriteTime != lastModifiedTime)
        {
            Console.WriteLine($"Using the latest data from {file.LastWriteTime}");
            cachedData = readSuburbData(filepath);
            lastModifiedTime = file.LastWriteTime;

        }

        return cachedData;
    }
    private Dictionary<int,SuburbRecord> readSuburbData(String filepath)
    {

        var data = new Dictionary<int, SuburbRecord>();

        foreach (var line in File.ReadLines(filepath))
        {

            var columns = line.Split(',');

            if(!int.TryParse(columns[0], out int result)) {
                continue;
            }

            var record = new SuburbRecord 
            {
                Id = Convert.ToInt32(columns[0]),
                Suburb = columns[1],
                Value = double.Parse(columns[2]),
                Date = columns[3],
                NumberOfBedrooms = Convert.ToInt32(columns[4]),
                Type = columns[5],

            };

            data[record.Id] = record;
        }

        return data;
    }

}