using System;
using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class TransformFilter : TransformFilter<FreeTransformer, EmptyParameters>
    {
        public TransformFilter(string name, Func<Size, Size> sizeTransform, Func<Point, Size, Point?> pointTransform) :
            base(name, new FreeTransformer(sizeTransform, pointTransform))
        {
        }
    }
}