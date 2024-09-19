// See https://aka.ms/new-console-template for more information
using Slugify;

var polishChars = "ĄĆĘŁŃÓŚŹŻąćęłńóśźż";
var transfChars = "ACELNOSZZacelnoszz";

var config = new SlugHelperConfiguration();

// Add individual replacement rules
//config.StringReplacements.Add("Ł", "L");
config.StringReplacements.Add("ł", "l");

// Keep the casing of the input string
//config.ForceLowerCase = false;

SlugHelper helper = new(config);
var i = 0;
foreach (var item in polishChars)
{
    var l = helper.GenerateSlug(item.ToString());
    var x = transfChars[i++].ToString().ToLower();
    if (l.Equals(x).Equals(false))
    {
        Console.WriteLine($"Błąd dla litery '{item}': {x} != '{l}'");
        break;
    }
}
if (i.Equals(polishChars.Length))
    Console.WriteLine($"Wszystko jest OK");
