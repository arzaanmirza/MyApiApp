public class SuburbInfo
{
    
    public string? Name { get; set;} = string.Empty;

    public int Units { get; set;}

    public int Houses { get; set;}


    public SuburbInfo(string name, int units, int houses)
    {
        Name = name;
        Units = units;
        Houses = houses;
    }

}