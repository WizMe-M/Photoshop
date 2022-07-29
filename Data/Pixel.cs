using System;

namespace MyPhotoshop.Data
{
    public struct Pixel
    {
        private double _r;
        private double _g;
        private double _b;

        public Pixel(double r, double g, double b) : this()
        {
            R = r;
            G = g;
            B = b;
        }

        public double R
        {
            get => _r;
            set => _r = SetValue(value);
        }

        public double G
        {
            get => _g;
            set => _g = SetValue(value);
        }

        public double B
        {
            get => _b;
            set => _b = SetValue(value);
        }

        private static double SetValue(double value)
        {
            if (value < 0) return 0;
            if (value > 1) return 1;
            return value;
        }


        public static Pixel operator *(Pixel px, double value)
            => new Pixel(px.R * value, px.G * value, px.B * value);


        public static Pixel operator *(double value, Pixel px) => px * value;
    }
}