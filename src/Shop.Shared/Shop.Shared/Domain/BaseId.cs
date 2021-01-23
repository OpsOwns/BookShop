using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Dawn;

namespace Shop.Shared.Domain
{
    public class BaseId : ValueObject
    {
        private const int DeffaultIdValue = 0;
        public int Value { get; private set; }
        protected BaseId() { }
        public BaseId(int id) : this()
        {
            Guard.Argument(id, nameof(id)).NotNegative();
            Value = id;
        }
        public static BaseId DefaultId => new BaseId() { Value = DeffaultIdValue };
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
