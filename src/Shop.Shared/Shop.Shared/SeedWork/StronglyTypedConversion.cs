using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Shop.Shared.SeedWork
{
    public static class StronglyTypedConversion
    {
        public static void AddStronglyTypedIdConversions(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (!StronglyTypedId.IsStronglyTypedId(property.ClrType, out var valueType)) continue;
                    var converter = StronglyTypedIdConverters.GetOrAdd(
                        property.ClrType,
                        _ => CreateStronglyTypedIdConverter(property.ClrType, valueType));
                    property.SetValueConverter(converter);
                }
            }
        }

        private static readonly ConcurrentDictionary<Type, ValueConverter> StronglyTypedIdConverters = new();

        private static ValueConverter CreateStronglyTypedIdConverter(
            Type stronglyTypedIdType,
            Type valueType)
        {
            var toProviderFuncType = typeof(Func<,>)
                .MakeGenericType(stronglyTypedIdType, valueType);
            var stronglyTypedIdParam = Expression.Parameter(stronglyTypedIdType, "id");
            var toProviderExpression = Expression.Lambda(
                toProviderFuncType,
                Expression.Property(stronglyTypedIdParam, "Value"),
                stronglyTypedIdParam);

            var fromProviderFuncType = typeof(Func<,>)
                .MakeGenericType(valueType, stronglyTypedIdType);
            var valueParam = Expression.Parameter(valueType, "value");
            var ctor = stronglyTypedIdType.GetConstructor(new[] { valueType });
            var fromProviderExpression = Expression.Lambda(
                fromProviderFuncType,
                Expression.New(ctor!, valueParam),
                valueParam);

            var converterType = typeof(ValueConverter<,>)
                .MakeGenericType(stronglyTypedIdType, valueType);

            return (ValueConverter)Activator.CreateInstance(
                converterType,
                toProviderExpression,
                fromProviderExpression,
                null);
        }
    }
}
