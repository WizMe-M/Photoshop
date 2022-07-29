using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyPhotoshop.Filters.Parameters
{
    public class ExpressionParameterHandler<TParameters> : IParametersHandler<TParameters> 
        where TParameters : IParameters, new()
    {
        private readonly PropertyInfo[] _properties;
        private readonly Func<double[], TParameters> _parser;

        public ExpressionParameterHandler()
        {
            _properties = typeof(TParameters)
                .GetProperties()
                .Where(info => info.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();

            var values = Expression.Parameter(typeof(double[]), "values");

            var bindings = new List<MemberBinding>();
            for (var i = 0; i < _properties.Length; i++)
            {
                var binding = Expression.Bind(
                    _properties[i],
                    Expression.ArrayIndex(values, Expression.Constant(i)));
                bindings.Add(binding);
            }

            var body = Expression.MemberInit(
                Expression.New(typeof(TParameters)),
                bindings);

            var lambda = Expression.Lambda<Func<double[], TParameters>>(body, values);
            _parser = lambda.Compile();
        }

        public ParameterInfo[] GetDescription() => _properties
            .Select(property => property.GetCustomAttributes(typeof(ParameterInfo), false))
            .Where(attributes => attributes.Length > 0)
            .Select(attributes => attributes[0])
            .Cast<ParameterInfo>()
            .ToArray();

        public TParameters CreateParameters(double[] values) => _parser(values);
    }
}