using System.Linq;
using System.Reflection;

namespace MyPhotoshop.Filters.Parameters
{
    public class SimpleParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        private readonly PropertyInfo[] _properties;

        public SimpleParametersHandler()
        {
            _properties = typeof(TParameters)
                .GetProperties()
                .Where(info => info.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();
        }

        public ParameterInfo[] GetDescription() => _properties
            .Select(property => property.GetCustomAttributes(typeof(ParameterInfo), false))
            .Where(attributes => attributes.Length > 0)
            .Select(attributes => attributes[0])
            .Cast<ParameterInfo>()
            .ToArray();

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