using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CamillaDsp.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var client = new CamillaDspClient("ws://192.168.1.123:1234");
        var sb = new StringBuilder();
        var sw = Stopwatch.StartNew();
        
        // Fetch some info
        sb.AppendLine($"camilladsp version: {await client.GetVersionAsync()}");
        sb.AppendLine($"config: {await client.GetConfig()}");
        var volume = await client.GetVolume();
        sb.AppendLine($"volume: {volume}dB");
        
        await client.SetVolume(volume.GetValueOrDefault() - 0.01f);
        
        sb.AppendLine($"volume: {await client.GetVolume()}dB");
        sb.AppendLine($"uodate interval: {await client.GetUpdateIntervalAsync()}ms");
        sb.AppendLine($"config file: {await client.GetConfigFilePath()}");
        sb.AppendLine($"config title: {await client.GetConfigTitle()}");
        sb.AppendLine($"elapsed: {sw.ElapsedMilliseconds}ms");
        Console.WriteLine(sb.ToString());
    }
}