using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
    public class Data
    {
        public int height { get; set; }
        public bool is_silhouette { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }

class program
{
    private static readonly HttpClient cliente = new HttpClient();
    static async Task Main()
    {
        Console.WriteLine("#---- Logo Facebook ----#");
        await GetInfo();
    }
    private static async Task GetInfo()
    {
        var url="https://graph.facebook.com/facebook/picture?redirect=false";
        HttpResponseMessage respuesta = await cliente.GetAsync(url);
        respuesta.EnsureSuccessStatusCode();
        string respuestaBody= await respuesta.Content.ReadAsStringAsync();
        //es un objeto,no una lista --> no defino List<Root> ...
        Root info=JsonSerializer.Deserialize<Root>(respuestaBody);
        Console.WriteLine($"Alto: {info.data.height} px | Ancho: {info.data.width} px | URL: {info.data.url}");
        File.WriteAllText("LogoFacebook.json",respuestaBody);
        Console.WriteLine("Se creo exitosamente el Json!");
    }
    
}