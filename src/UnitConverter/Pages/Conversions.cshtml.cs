using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitOf;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    public string Input { get; set; } = string.Empty;

    public string Output { get; set; } = string.Empty;


    public void OnGet()
    {
        Input = "3.1415";
        ViewData["ConversionType"] = "Miles to Kilometers";
        ViewData["Title"] = "Conversions";
        double Miles = Convert.ToDouble(Input);
        double Kilometers = new Length().FromMiles(Miles).ToKilometers();
        Output = Kilometers.ToString();
    }
}
