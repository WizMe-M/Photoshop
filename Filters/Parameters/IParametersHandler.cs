namespace MyPhotoshop.Filters.Parameters
{
    public interface IParametersHandler<out TParameters>
        where TParameters : IParameters
    {
        ParameterInfo[] GetDescription();
        TParameters CreateParameters(double[] values);
    }
}