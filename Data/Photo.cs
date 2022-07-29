namespace MyPhotoshop.Data
{
    public class Photo
    {
        private readonly Pixel[,] _data;

        public int Width { get; }
        public int Height { get; }

        public Pixel this[int x, int y]
        {
            get => _data[x, y];
            set => _data[x, y] = value;
        }

        public Photo(int width, int height)
        {
            Width = width;
            Height = height;
            _data = new Pixel[Width, Height];
        }
    }
}