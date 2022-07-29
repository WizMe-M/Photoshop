using System;
using MyPhotoshop.Data;
using MyPhotoshop.Filters.Parameters;

namespace MyPhotoshop.Filters
{
    public class PixelFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        private readonly Func<Pixel, TParameters, Pixel> _pixelProcessor;

        public PixelFilter(string name, Func<Pixel, TParameters, Pixel> pixelProcessor) : base(name)
        {
            _pixelProcessor = pixelProcessor;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var result = new Photo(original.Width, original.Height);

            for (var x = 0; x < result.Width; x++)
            for (var y = 0; y < result.Height; y++)
            {
                result[x, y] = _pixelProcessor(original[x, y], parameters);
            }

            return result;
        }

    }
}