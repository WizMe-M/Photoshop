using System.Drawing;
using MyPhotoshop.Filters.Parameters;

namespace MyPhotoshop.Filters.Transform
{
    public interface ITransformer<in TParameters>
        where TParameters : IParameters, new()
    {
        /// <summary>
        /// Размер преобразованного изображения
        /// </summary>
        Size ResultSize { get; }
        
        /// <summary>
        /// Подгатавливает изображение к трансформации 
        /// </summary>
        void Prepare(Size oldSize, TParameters parameters);
        
        /// <summary>
        /// Сопоставляет новую точку со старой
        /// </summary>
        /// <returns>Старые координаты точки</returns>
        Point? MapPoint(Point newPoint);
    }
}