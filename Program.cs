using System.Collections.Generic;

Console.WriteLine("Checkpoint 3");
Console.WriteLine("Week 3 Inheritance and DateTime");
Console.WriteLine("---------------------------");
Console.WriteLine("My assets - computers and phones");
//Console.WriteLine("Press q to show the list");


//List of assets
List<Asset> assets = new List<Asset>()
{
    new Computer("Computer","HP", "Elitebook", "Spain", Convert.ToDateTime("2018-12-10"), 1423),
    new Computer("Computer","HP", "Select", "Denmark", Convert.ToDateTime("2021-09-15"), 723),
    new Computer("Computer","Mac", "MacBook Pro", "Spain", Convert.ToDateTime("2017-11-20"), 1723),
    new Computer("Computer","Asus", "W234", "Sweden",Convert.ToDateTime("2020-03-14"), 823),
    new Computer("Computer","Mac", "MacBook Air", "Sweden", Convert.ToDateTime("2022-04-11"), 2423),
    new Computer("Computer","Lenovo", "Yoga 730", "Denmark", Convert.ToDateTime("2020-05-03"), 1320),
    new Phone("Phone","iPhone", "8", "Spain", Convert.ToDateTime("2019-12-10"), 970),
    new Phone("Phone","iPhone", "11", "Sweden", Convert.ToDateTime("2021-01-12"), 1130),
    new Phone("Phone","Samsung", "Galaxy", "Spain", Convert.ToDateTime("2022-02-03"), 760),
    new Phone("Phone", "Samsung", "Noir", "Spain", Convert.ToDateTime("2017-06-11"), 600),
    new Phone("Phone","iPhone", "13", "Denmark", Convert.ToDateTime("2020-08-08"), 1240),
    new Phone("Phone","Motorola", "G22", "Sweden", Convert.ToDateTime("2021-02-11"), 208),
};


//Currency still to add - euros to kronor


//List sorted by price
//List<Asset> sortedAssets = assets.OrderBy(asset => asset.Price).ToList();

//List sorted by type, computer first
//List<Asset> sortedAssets = assets.OrderBy(asset => asset.Type).ToList();

//List sorted by purchase date
//List<Asset> sortedAssets = assets.OrderBy(asset => asset.Date).ToList();

//List sorted by asset type and purchase date
static List<Asset> SortedAssetsByTypeAndDate(List<Asset> assets)
{
    assets = assets.OrderBy(asset => asset.GetType().Name).ThenBy(asset => asset.Date).ToList();
    return assets;
}

static List<Asset> SortedByCountryAndDate(List<Asset> assets)
{
    assets = assets.OrderBy(asset => asset.Country).ThenBy(asset => asset.Date).ToList();
    return assets;
}

// Converts date from string to DateTime
// <returns>DateTime</returns>
static DateTime GetDate(string date)
{
    return DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
}

//ToShortDateString = Date whitout time
//List Sorted by first office, then date, then type

List<Asset> sortedList = SortedAssetsByTypeAndDate(assets);
List<Asset> sortedListByOffice = SortedByCountryAndDate(sortedList);
Console.WriteLine("If red or yellow - check expiration date");
Console.WriteLine("---------------------------");

Console.WriteLine("Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Country".PadRight(20) + "Date".PadRight(20) + "Price");

//If today is closer than 3 months from expiring date show color yellow or red
//Date variable and todays date = how many days diffrence = convert to int
//if more than 990 days (or 3 months) mark with yellow
//if more than 1080 days mark with red

//Days between puchase date and today to choose color
int daysWarning = 990; //Approx 33 months - yellow
int daysAlarm = 1080;  //Approx 36 months - red

foreach (Asset asset in sortedListByOffice)
{
  TimeSpan diff = DateTime.Now - asset.Date;//Calculate time span between today and purchase date
  DecideForegroundColor(daysWarning, daysAlarm, diff);//Decide right color according to date
  Console.WriteLine(asset.Type.PadRight(20) + asset.Brand.PadRight(20) + asset.Model.PadRight(20) + asset.Country.PadRight(20) + asset.Date.ToShortDateString().PadRight(20) + asset.Price);
}

static void DecideForegroundColor(int daysWarning, int daysAlarm, TimeSpan diff)
{
    if (diff.Days > daysAlarm)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    else if (diff.Days > daysWarning)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.White;
    }
}

int sum = assets.Sum(asset => asset.Price);
Console.WriteLine("Total Sum: ".PadRight(100) + sum);


class Asset // Base class, super class, parent class
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Country { get; set; }

    public DateTime Date { get; set; }

    public int Price { get; set; }
}

class Computer : Asset //Derived class from Asset
{

    public Computer(string type, string brand, string model, string country, DateTime date, int price)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Country = country;
        Date = date;
        Price = price;

    }
}

//Derived class from Asset
class Phone : Asset
{
    public Phone(string type, string brand, string model, string country, DateTime date, int price)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Country = country;
        Date = date;
        Price = price;

    }
}