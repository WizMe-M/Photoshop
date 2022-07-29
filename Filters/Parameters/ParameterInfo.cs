using System;

namespace MyPhotoshop.Filters.Parameters
{
    /// <summary>
    /// Этот класс содержит описание одного параметра фильтра: как он называется, в каких пределах изменяется, и т.д.
    /// Эта информация необходима для настройки графического интерфейса.
    /// </summary>
    public class ParameterInfo : Attribute
    {
        public string Name;
        public double DefaultValue;
        public double MinValue;
        public double MaxValue;
        public double Increment;

        public ParameterInfo(string name, double minValue, double maxValue, double defaultValue, double increment)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            DefaultValue = defaultValue;
            Increment = increment;
        }
    }
}
