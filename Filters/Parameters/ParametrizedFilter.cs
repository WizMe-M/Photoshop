using MyPhotoshop.Data;

namespace MyPhotoshop.Filters.Parameters
{
    public abstract class ParametrizedFilter<TParameters> : IFilter
        where TParameters : IParameters, new()
    {
        private readonly IParametersHandler<TParameters> _handler = new SimpleParametersHandler<TParameters>();
        private readonly string _name;

        public ParametrizedFilter(string name) => _name = name;

        public abstract Photo Process(Photo original, TParameters parameters);

        ParameterInfo[] IFilter.GetParameters()
        {
            return _handler.GetDescription();
        }

        public Photo Process(Photo original, double[] values)
        {
            var parameters = _handler.CreateParameters(values);
            return Process(original, parameters);
        }
        
        public override string ToString() => _name;
    }
}