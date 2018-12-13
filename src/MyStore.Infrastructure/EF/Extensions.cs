using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyStore.Core;

namespace MyStore.Infrastructure.EF
{
    public static class Extensions
    {
        public static PropertyBuilder<T> HasAggregateIdConversion<T>(this PropertyBuilder<T> builder)
            => builder.HasConversion(new AggregateIdValueConverter());

        private class AggregateIdValueConverter : ValueConverter<AggregateId, Guid>
        {
            public AggregateIdValueConverter() : this(p => p.Value, p => new AggregateId(p))
            {
            }

            private AggregateIdValueConverter(Expression<Func<AggregateId, Guid>> convertToProviderExpression,
                Expression<Func<Guid, AggregateId>> convertFromProviderExpression,
                ConverterMappingHints mappingHints = null) :
                base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
            {
            }
        }
    }
}