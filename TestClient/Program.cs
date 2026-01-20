using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CamillaDsp.Client;
using CamillaDsp.Client.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        var client = new CamillaDspClient("ws://192.168.1.123:1234");
        Debug.WriteLine($"Version: {await client.GetVersionAsync()}");
    }
}