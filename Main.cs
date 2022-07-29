using System;
using System.Drawing;
using System.Windows.Forms;
using MyPhotoshop.Data;
using MyPhotoshop.Filters;
using MyPhotoshop.Filters.Transform;

namespace MyPhotoshop
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var window = new MainWindow();

            window.AddFilter(new PixelFilter<LighteningParameters>(
                "Осветление/затемнение",
                (px, parameters) => px * parameters.Coefficient)
            );

            window.AddFilter(new PixelFilter<EmptyParameters>(
                "Черно-белый фильтр",
                (px, _) =>
                {
                    var gray = px.R * .299 + px.G * .587 + px.B * .144;
                    return new Pixel(gray, gray, gray);
                }));

            window.AddFilter(new TransformFilter(
                "Отразить по горизонтали",
                size => size,
                (point, size) => new Point(size.Width - point.X - 1, point.Y)));

            window.AddFilter(new TransformFilter(
                "Поворот на 90 градусов",
                size => new Size(size.Height, size.Width),
                (point, _) => new Point(point.Y, point.X)));


            window.AddFilter(
                new TransformFilter<FreeRotateTransformer, FreeRotationParameters>(
                    "Свободное вращение", new FreeRotateTransformer()));

            Application.Run(window);
        }
    }
}