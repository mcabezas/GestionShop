namespace HTTP.Common
{
    public class MaybeEmpty<T>
    {
        private T _value;
        public bool Found { get; private set; } = false;

        private MaybeEmpty()
        {
        }

        public static MaybeEmpty<T> Empty()
        {
            return new MaybeEmpty<T>();
        }

        public static MaybeEmpty<T> Of(T value)
        {
            MaybeEmpty<T> obj = new MaybeEmpty<T>();
            obj.Set(value);
            return obj;
        }

        public void Set(T value)
        {
            _value = value;
            Found = true;
        }

        public T Get()
        {
            return _value;
        }
    }
}