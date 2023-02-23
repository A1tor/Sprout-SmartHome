using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont("CustomFont.ttf", Alias = "CF")]
[assembly: ExportFont("Font Awesome 6 Brands-Regular-400.otf", Alias = "Brands")]
[assembly: ExportFont("Font Awesome 6 Free-Regular-400.otf", Alias = "Regular")]
[assembly: ExportFont("Font Awesome 6 Free-Solid-900.otf", Alias = "Solid")]
[assembly: NeutralResourcesLanguage("ru")]