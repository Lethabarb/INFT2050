// See https://aka.ms/new-console-template for more information
using NAudio.Wave;
using System.Runtime.InteropServices;

internal class Program
{
    static int SampleRate = 44100;
    private static void Main(string[] args)
    {
        Console.WriteLine();
        WaveInEvent WaveIn = new WaveInEvent()
        {
            DeviceNumber = 0,
            WaveFormat = new WaveFormat(44100, 16, 1),
            BufferMilliseconds = 1000
        };
        int deviceCount = WaveInEvent.DeviceCount;
        for (int i = 0; i < deviceCount; i++)
        {
            Console.WriteLine(WaveInEvent.GetCapabilities(i).ProductName);
        }


        WaveIn.DataAvailable += (sender, args) =>
        {
            var b = new byte[args.BytesRecorded];
            var f = MemoryMarshal.Cast<byte, float>(args.Buffer);
                var freq = GoertzelFilter(f.ToArray(), 400);
            // print freq in hz
            Console.WriteLine($"\r{freq}");
        };
        WaveIn.StartRecording();
        while (true)
        {
            var x = 1;
        }
    }
    private static double GoertzelFilter(float[] samples, double freq)
    {
        double sPrev = 0.0;
        double sPrev2 = 0.0;
        int i;
        double normalizedfreq = freq / SampleRate;
        double coeff = 2 * Math.Cos(2 * Math.PI * normalizedfreq);
        for (i = 0; i < samples.Length; i++)
        {
            double s = samples[i] + coeff * sPrev - sPrev2;
            sPrev2 = sPrev;
            sPrev = s;
        }
        double power = sPrev2 * sPrev2 + sPrev * sPrev - coeff * sPrev * sPrev2;
        return power;
    }
}