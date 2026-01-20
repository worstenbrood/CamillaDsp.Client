using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CamillaDsp.Client;
using CamillaDsp.Client.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        var client = new CamillaDspClient("ws://192.168.1.123:1234", true);
        var sw = Stopwatch.StartNew();
        Console.WriteLine($"camilladsp version: {await client.GetVersionAsync()}");
        Console.WriteLine($"volume: {await client.GetVolume()}dB");
        Console.WriteLine($"elapsed: {sw.ElapsedMilliseconds}ms");
    }
}