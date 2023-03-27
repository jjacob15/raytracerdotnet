using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RayTracer
{
    public class RendererStats
    {
        public double Progress => (double) CompletedPixels / TotalPixels;

        public RendererStats(int totalPixels)
        {
            StartTime = DateTime.Now;
            TotalPixels = totalPixels;
        }

        private int TotalPixels { get; }
        private int CompletedPixels = 0;

        private DateTime StartTime { get; set; }
        private DateTime StopTime { get; set; } = DateTime.MinValue;

        public TimeSpan Time => (StopTime == DateTime.MinValue ? DateTime.Now : StopTime) - StartTime;
        public double Speed => CompletedPixels / Time.TotalSeconds;

        public void IncrementPixelCount()
        {
            Interlocked.Increment(ref CompletedPixels);
        }

        public void Stop()
        {
            StopTime = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{Time:hh\\:mm\\:ss}, {Progress:p2}, {Speed:n2} px/s";
        }
    }
}
