﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAssertions.Formatting
{
    /// <summary>
    /// Provides services for formatting an object being used in an assertion in a human readable format.
    /// </summary>
    public static class Formatter
    {
        /// <summary>
        /// A list of objects responsible for formatting the objects represented by placeholders.
        /// </summary>
        private static readonly List<IValueFormatter> formatters = new List<IValueFormatter>
        {
            new NullValueFormatter(),
            new DateTimeValueFormatter(),
            new TimeSpanValueFormatter(),
            new NumericValueFormatter(),
            new StringValueFormatter(),
            new ExpressionValueFormatter(),
            new EnumerableValueFormatter(),
#if !SILVERLIGHT
            new XDocumentValueFormatter(),
            new XElementValueFormatter(),
            new XAttributeValueFormatter(),
#endif
            new DefaultValueFormatter(),
        };

        /// <summary>
        /// Returns a human-readable representation of a particular object.
        /// </summary>
        /// <param name="value">The value for which to create a <see cref="System.String"/>.</param>
        /// <param name="nestedPropertyLevel">
        /// The level of nesting for the supplied value. This is used for indenting the format string for objects that have
        /// no <see cref="object.ToString()"/> override.
        /// </param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(object value, int nestedPropertyLevel = 0)
        {
            var firstFormatterThatCanHandleValue = formatters.First(f => f.CanHandle(value));

            return firstFormatterThatCanHandleValue.ToString(value, nestedPropertyLevel);
        }

        /// <summary>
        /// Returns a human-readable representation of a particular object that starts on a new line.
        /// </summary>
        /// <param name="value">The value for which to create a <see cref="System.String"/>.</param>
        /// <param name="nestedPropertyLevel">
        /// The level of nesting for the supplied value. This is used for indenting the format string for objects that have
        /// no <see cref="object.ToString()"/> override.
        /// </param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToStringLine(object value, int nestedPropertyLevel = 0)
        {
            return Environment.NewLine + ToString(value, nestedPropertyLevel);
        }
    }
}
