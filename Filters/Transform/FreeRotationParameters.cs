using MyPhotoshop.Filters.Parameters;

namespace MyPhotoshop.Filters.Transform
{
    public class FreeRotationParameters : IParameters
    {
        [ParameterInfo("Угол поворота", 0, 360, 90, 1)]
        public double Angle { get; set; }
    }
}