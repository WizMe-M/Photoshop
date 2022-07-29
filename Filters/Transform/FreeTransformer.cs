using System;
using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class FreeTransformer : ITransformer<EmptyParameters>
    {
        private Size _oldSize;

        /// <summary>
        /// Преобразует старый размер в новый
        /// </summary>
        private readonly Func<Size, Size> _sizeTransform;

        /// <summary>
        /// Сопоставляет новую точку со старой исходя из старого размера
        /// </summary>
        private readonly Func<Point, Size, Point?> _pointTransform;

        public FreeTransformer(Func<Size, Size> sizeTransform, Func<Point, Size, Point?> pointTransform)
        {
            _sizeTransform = sizeTransform;
            _pointTransform = pointTransform;
        }

        /// <inheritdoc/>
        public Size ResultSize { get; private set; }

        public void Prepare(Size oldSize, EmptyParameters parameters)
        {
            _oldSize = oldSize;
            ResultSize = _sizeTransform(_oldSize);
        }

        public Point? MapPoint(Point newPoint) => _pointTransform(newPoint, _oldSize);
    }
}