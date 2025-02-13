// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json.Linq;

var example = File.ReadAllText("example.json");

var jObject = JObject.Parse(example);

var items = Get(jObject, []);

List<string> Get(JObject jObject, List<string> values)
{
    foreach (var item in jObject)
    {
        if (item.Value is JObject)
        {
            if (item.Value["nodeType"]?.ToString() == "text")
            {
                values.Add(item.Value.Path.ToString());
                return values;
            }
            
            Get((JObject)item.Value, values);
        }

        if (item.Value is JArray)
        {
            foreach (var arrayItem in (JArray)item.Value)
            {
                if (arrayItem is JObject)
                {
                    if (arrayItem["nodeType"]?.ToString() == "text")
                    {
                        values.Add(arrayItem.Path.ToString());
                        return values;
                    }
                    
                    Get((JObject)arrayItem, values);
                }
            }
        }
    }

    return values;
}

foreach (var item in items)
{
    Console.WriteLine($"Path: {item}");
    Console.WriteLine($"Value: {jObject.SelectToken(item).Value<string>("value")}");
}

Console.WriteLine("End");