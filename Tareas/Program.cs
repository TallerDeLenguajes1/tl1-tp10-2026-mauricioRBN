using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

public class Tarea
{
    public int userId{get; set;}
    public int id{get; set;}
    public string title{get; set;}
    public bool completed{get; set;}
}
class program
{
    private static readonly HttpClient cliente = new HttpClient();
    
    static async Task Main()
    {
        await GetTarea();
    }
    private static async Task GetTarea()
    {
        //obtengo datos del json
        var url="https://jsonplaceholder.typicode.com/todos/";
        HttpResponseMessage respuesta = await cliente.GetAsync(url);  //peticion get al servidor(espera sin bloquearla)
        respuesta.EnsureSuccessStatusCode(); //si la respuesta no fue exitosa da error 404 o 500 (Es el chequeo de seguridad)
        string respuestaBody= await respuesta.Content.ReadAsStringAsync(); //convierte el Json en string
        List<Tarea> listaTareas=JsonSerializer.Deserialize<List<Tarea>>(respuestaBody);
        //creo listas para ordenar los datos obtenidos
        List<Tarea> Pendientes=new List<Tarea>();
        List<Tarea> Completadas=new List<Tarea>();
        foreach(var t in listaTareas)
        {
            if (t.completed)
            {
                Completadas.Add(t);
            }
            else
            {
                Pendientes.Add(t);
            }
        }
        //muestro por pantalla
        Console.WriteLine("#-------TAREAS PENDIENTES-------#");
        foreach(var t in Pendientes)
        {
            Console.WriteLine($"Titulo:{t.title} | Estado:{t.completed}");

        }
        Console.WriteLine("#--------TAREAS REALIZADAS-------#");
        foreach(var t in Completadas)
        {
            Console.WriteLine($"Titulo:{t.title} | Estado:{t.completed}");
            
        }
        //Creo nuevo archivo Json
        var opciones =new JsonSerializerOptions{WriteIndented=true};
        string nuevoJson=JsonSerializer.Serialize(listaTareas,opciones);
        //como es una linea de texto...
        File.WriteAllText("MiPrimer.json",nuevoJson);
        Console.WriteLine("Guardado Exitoso!");

    }

}

