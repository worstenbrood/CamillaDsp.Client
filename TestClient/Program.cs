using CamillaDsp.Client;
using System;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        var client = new CamillaDspClient("ws://192.168.1.123:1234");
        Debug.WriteLine($"Version: {client.GetVersionAsync().ConfigureAwait(false).GetAwaiter().GetResult()}");
        client.SetUpdateIntervalAsync(1000).ConfigureAwait(false).GetAwaiter().GetResult();
        Debug.WriteLine($"UpdateInterval: {client.GetUpdateIntervalAsync().ConfigureAwait(false).GetAwaiter().GetResult()}");
    }
}