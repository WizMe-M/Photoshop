using System;
using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class FreeRotateTransformer : ITransformer<FreeRotationParameters>
    {
        /// <inheritdoc/>
        public Size ResultSize { get; private set; }

        /// <summary>
        /// Изначальный размер изображения
        /// </summary>
        public Size OriginalSize { get; private set; }

        /// <summary>
        /// Угол в радианах
        /// </summary>
        public double RadianAngle { get; private set; }


        public void Prepare(Size oldSize, FreeRotationParameters parameters)
        {
            OriginalSize = oldSize;
            RadianAngle = Math.PI * parameters.Angle / 180;
            ResultSize = new Size(
                (int)(oldSize.Width * Math.Abs(Math.Cos(RadianAngle)) +
                      oldSize.Height * Math.Abs(Math.Sin(RadianAngle))),
                (int)(oldSize.Height * Math.Abs(Math.Cos(RadianAngle)) +
                      oldSize.Width * Math.Abs(Math.Sin(RadianAngle))));
        }

        public Point? MapPoint(Point newPoint)
        {
            newPoint = new Point(newPoint.X - ResultSize.Width / 2, newPoint.Y - ResultSize.Height / 2);
            var x = OriginalSize.Width / 2 +
                    (int)(newPoint.X * Math.Cos(RadianAngle) + newPoint.Y * Math.Sin(RadianAngle));
            var y = OriginalSize.Height / 2 +
                    (int)(-newPoint.X * Math.Sin(RadianAngle) + newPoint.Y * Math.Cos(RadianAngle));
            if (x < 0 || x >= OriginalSize.Width || y < 0 || y >= OriginalSize.Height) return null;
            return new Point(x, y);
        }
    }
}