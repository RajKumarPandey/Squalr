﻿namespace SqualrCore.Source.Utils.TypeConverters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using Utils;

    /// <summary>
    /// Data type converter for use in the property viewer.
    /// </summary>
    public class DataTypeConverter : StringConverter
    {
        /// <summary>
        /// Gets the standard collection of values to display in a drop down.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <returns>The standard collection of values.</returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(PrimitiveTypes.GetScannablePrimitiveTypes().Select(x => x.ToString()).ToList());
        }

        /// <summary>
        /// Gets a value indicating whether standard values are supported.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <returns>True if standard values are supported.</returns>
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Converts a string to the corresponding value type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="culture">Globalization info.</param>
        /// <param name="value">The value being converted.</param>
        /// <returns>The converted value.</returns>
        public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
        {
            foreach (DataType type in PrimitiveTypes.GetScannablePrimitiveTypes())
            {
                if ((Object)type == null || !(value is String))
                {
                    break;
                }

                if (type.ToString() == value as String)
                {
                    return type;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Determines if this converter can convert to the given source type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>True if this converter can convert to the given type.</returns>
        public override Boolean CanConvertTo(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        /// <summary>
        /// Determines if this converter can convert from the given source type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>True if this converter can convert from the given type.</returns>
        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == null || sourceType == typeof(String))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }
    }
    //// End class
}
//// End namespace