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
        var volume = await client.GetVolume();
        sb.AppendLine($"volume: {volume}dB");
        
        await client.SetVolume(volume.GetValueOrDefault() + 0.01f);
        
        sb.AppendLine($"volume: {await client.GetVolume()}dB");
        sb.AppendLine($"config: {await client.GetConfig()}");
        sb.AppendLine($"elapsed: {sw.ElapsedMilliseconds}ms");
        Console.WriteLine(sb.ToString());
    }
}