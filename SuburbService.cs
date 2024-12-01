public class SuburbService
{


    private readonly LoadingData _loadingData;
    public SuburbService(LoadingData loadingData)
    {
        this._loadingData = loadingData;
    }

    private const string FILEPATH = "PropertyValuesTest.csv";
    public List<SuburbRecord> getProperty()
    {

        try 
        {
            
            var suburbs = _loadingData.loadSuburbData(FILEPATH);
            return new List<SuburbRecord>(suburbs.Values);
        }
        catch (Exception ex)
        {
            return new List<SuburbRecord>();
        }
    }

    public List<SuburbInfo> getSuburbInfo()
    {

        var data = new Dictionary<string, SuburbData>();
        var responseData = new List<SuburbInfo>();
        var suburbs = _loadingData.loadSuburbData(FILEPATH);

        foreach (var item in suburbs.Values)
        {

            if(!data.ContainsKey(item.Suburb))
            {
                data[item.Suburb] = new SuburbData();

            }

            if(item.Type == "house")
            {

                data[item.Suburb].HouseValue += item.Value;
                data[item.Suburb].HouseCount += 1;
            }
            else
            {
                data[item.Suburb].UnitValue += item.Value;
                data[item.Suburb].UnitCount += 1;
            }

        }

        foreach(var suburb in data)
        {

            int averageUnitValue = (int)((suburb.Value.UnitValue / suburb.Value.UnitCount)*1000);
            int averageHouseValue = (int)((suburb.Value.HouseValue / suburb.Value.HouseCount)*1000);

            var suburbInfo = new SuburbInfo(
                suburb.Key,
                averageUnitValue,
                averageHouseValue
            );

            responseData.Add(suburbInfo);
        }

        return responseData;

    }

    public SuburbRecord? getPropertyId(string id)
    {
        try
        {
            var suburbs = _loadingData.loadSuburbData(FILEPATH);
            var property = suburbs[int.Parse(id)];
            return property;
        }
        catch (Exception)
        {
            return null;
        }

    }



}