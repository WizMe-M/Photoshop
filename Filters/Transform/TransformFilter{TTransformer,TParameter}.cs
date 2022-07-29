using System.Drawing;
using MyPhotoshop.Data;
using MyPhotoshop.Filters.Parameters;

namespace MyPhotoshop.Filters.Transform
{
    public class TransformFilter<TTransformer, TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new() 
        where TTransformer: ITransformer<TParameters>
    {
        private readonly TTransformer _transformer;
        
        public TransformFilter(string name, TTransformer transformer) : base(name)
        {
            _transformer = transformer;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.Width, original.Height);
            _transformer.Prepare(oldSize, parameters);
            var result = new Photo(_transformer.ResultSize.Width, _transformer.ResultSize.Height);

            for (var x = 0; x < result.Width; x++)
            for (var y = 0; y < result.Height; y++)
            {
                var currentPoint = new Point(x, y);
                var pointSource = _transformer.MapPoint(currentPoint);
                
                if (pointSource.HasValue)
                {
                    var p = pointSource.Value;
                    result[x, y] = original[p.X, p.Y];
                }
            }

            return result;
        }
    }
}