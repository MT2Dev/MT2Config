using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace MT2Config
{
    internal struct Resolution : IEquatable<Resolution>, IComparable<Resolution>
    {
        public Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public bool Equals(Resolution other) => Width == other.Width && Height == other.Height;
        public override bool Equals(object obj) => obj is Resolution && Equals((Resolution)obj);
        public override int GetHashCode() => Width * 65536 + Height;
        public int CompareTo(Resolution other) => Width != other.Width ? Width.CompareTo(other.Width) : Height.CompareTo(other.Height);
        public override string ToString() => Width + " × " + Height;
    }

    /// <summary>Resolutions and refresh rates (up to <see cref="GameClient.MaxRefreshRate"/>) the primary monitor supports.</summary>
    internal sealed class DisplayModes
    {
        // The client's interface needs at least 800x600.
        const int MinWidth = 800;
        const int MinHeight = 600;

        // Used when the modes cannot be queried.
        static readonly Resolution[] FallbackResolutions =
        {
            new Resolution(800, 600), new Resolution(1024, 768), new Resolution(1280, 720), new Resolution(1280, 1024),
            new Resolution(1366, 768), new Resolution(1600, 900), new Resolution(1920, 1080),
        };

        readonly SortedDictionary<Resolution, SortedSet<int>> modes = new SortedDictionary<Resolution, SortedSet<int>>();

        public IEnumerable<Resolution> Resolutions => modes.Keys;

        public IEnumerable<int> FrequenciesOf(Resolution resolution)
        {
            SortedSet<int> frequencies;
            return modes.TryGetValue(resolution, out frequencies) ? frequencies : Enumerable.Empty<int>();
        }

        public static DisplayModes Query()
        {
            var result = new DisplayModes();
            try
            {
                var mode = new NativeMethods.DEVMODE { dmSize = (short)Marshal.SizeOf(typeof(NativeMethods.DEVMODE)) };
                for (int i = 0; NativeMethods.EnumDisplaySettings(null, i, ref mode); i++)
                {
                    if (mode.dmBitsPerPel == 32 && (mode.dmDisplayFlags & NativeMethods.DM_INTERLACED) == 0
                        && mode.dmPelsWidth >= MinWidth && mode.dmPelsHeight >= MinHeight)
                    {
                        result.Add(new Resolution(mode.dmPelsWidth, mode.dmPelsHeight), mode.dmDisplayFrequency);
                    }
                }
            }
            catch (Exception e) when (e is DllNotFoundException || e is EntryPointNotFoundException)
            {
                // Not running on Windows.
            }

            if (result.modes.Count == 0)
            {
                foreach (Resolution resolution in FallbackResolutions)
                    result.Add(resolution, GameClient.MaxRefreshRate);
            }
            return result;
        }

        void Add(Resolution resolution, int frequency)
        {
            // Modes the client cannot use are left out; a resolution offered only above the limit is not listed at all.
            if (frequency > GameClient.MaxRefreshRate)
                return;

            SortedSet<int> frequencies;
            if (!modes.TryGetValue(resolution, out frequencies))
                modes.Add(resolution, frequencies = new SortedSet<int>());

            // 0 and 1 mean "hardware default" rather than a real refresh rate.
            if (frequency > 1)
                frequencies.Add(frequency);
        }
    }
}
