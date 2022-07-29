using MyPhotoshop.Filters.Parameters;

namespace MyPhotoshop.Filters
{
    public class LighteningParameters : IParameters
    {
        [ParameterInfo("Коэффициент", 0, 10, 1, 0.1)]
        public double Coefficient { get; set; }
    }
}