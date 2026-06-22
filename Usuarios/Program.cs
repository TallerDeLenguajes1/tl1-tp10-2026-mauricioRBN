using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public Geo geo { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }

    public class Geo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public Company company { get; set; }
    }
class program
{
    private static readonly HttpClient cliente=new HttpClient();
    static async Task Main()
    {
        await GetClientes();
    }
    private static async Task GetClientes()
    {
        var url="https://jsonplaceholder.typicode.com/users";
        HttpResponseMessage respuesta = await cliente.GetAsync(url);
        respuesta.EnsureSuccessStatusCode();
        string respuestaBody= await respuesta.Content.ReadAsStringAsync();
        List<Root> ListaClientes=JsonSerializer.Deserialize<List<Root>>(respuestaBody);
        int i=0;
        foreach(var usu in ListaClientes)
        {
            if (i == 5)
            {
                break;
            }
            Console.WriteLine($"Nombre: {usu.name} | Correo: {usu.email}| Domicilio: {usu.address.street}, {usu.address.suite}, {usu.address.city}");
            i++;
        }    
    }
}