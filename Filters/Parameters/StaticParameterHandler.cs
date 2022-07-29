using System.Linq;
using System.Reflection;

namespace MyPhotoshop.Filters.Parameters
{
    public class StaticParameterHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        private static PropertyInfo[] _properties;
        private static ParameterInfo[] _description;

        public StaticParameterHandler()
        {
            _properties = typeof(TParameters)
                .GetProperties()
                .Where(info => info.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();

            _description = _properties
                .Select(info => info.GetCustomAttributes(typeof(ParameterInfo), false))
                .Where(attributes => attributes.Length > 0)
                .Select(attributes => attributes.First())
                .Cast<ParameterInfo>()
                .ToArray();
        }

        public ParameterInfo[] GetDescription() => _description;

        public TParameters CreateParameters(double[] values)
        {
            var parameters = new TParameters();
            for (var i = 0; i < _properties.Length; i++)
            {
                _properties[i].SetValue(parameters, values[i], null);
            }

            return parameters;
        }
    }
}