using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Shop.Shared.Domain;

namespace Shop.Shared.SeedWork
{
    public static class StronglyTypedId
    {
        private static readonly ConcurrentDictionary<Type, Delegate> StronglyTypedIdFactories = new();

        public static Func<TValue, object> GetFactory<TValue>(Type stronglyTypedIdType)
            where TValue : notnull
        {
            return (Func<TValue, object>) StronglyTypedIdFactories.GetOrAdd(
                stronglyTypedIdType,
                CreateFactory<TValue>);
        }

        private static Func<TValue, object> CreateFactory<TValue>(Type stronglyTypedIdType)
            where TValue : notnull
        {
            if (!IsStronglyTypedId(stronglyTypedIdType))
                throw new ArgumentException($"Type '{stronglyTypedIdType}' is not a strongly-typed id type",
                    nameof(stronglyTypedIdType));

            var ctor = stronglyTypedIdType.GetConstructor(new[] {typeof(TValue)});
            if (ctor is null)
                throw new ArgumentException(
                    $"Type '{stronglyTypedIdType}' doesn't have a constructor with one parameter of type '{typeof(TValue)}'",
                    nameof(stronglyTypedIdType));

            var param = Expression.Parameter(typeof(TValue), "value");
            var body = Expression.New(ctor, param);
            var lambda = Expression.Lambda<Func<TValue, object>>(body, param);
            return lambda.Compile();
        }

        private static bool IsStronglyTypedId(Type type)
        {
            return IsStronglyTypedId(type, out _);
        }

        public static bool IsStronglyTypedId(Type type, [NotNullWhen(true)] out Type idType)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (type.BaseType is {IsGenericType: true} baseType &&
                baseType.GetGenericTypeDefinition() == typeof(BaseId<>))
            {
                idType = baseType.GetGenericArguments()[0];
                return true;
            }

            idType = null;
            return false;
        }
    }
}