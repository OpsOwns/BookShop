namespace Shop.Shared.Domain
{
    public abstract record BaseId<TValue>(TValue Value)
        where TValue : notnull
    {
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}