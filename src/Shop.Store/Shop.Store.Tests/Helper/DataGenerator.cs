using System.Collections.Generic;

namespace Shop.Book.Tests.Helper
{
    public static class DataGenerator
    {
        public static IEnumerable<object[]> GetInvalidIsbn10Code()
        {
            yield return new object[] {"ISBN 158182-008-9"};
            yield return new object[] {"ISBN 2257-7"};
            yield return new object[] {"ISBN 97854674562"};
        }

        public static IEnumerable<object[]> GetInvalidIsbn13Code()
        {
            yield return new object[] {"ISBN 964-aa-158182-008-9"};
            yield return new object[] {"ISBN bfgfdg-7"};
            yield return new object[] {"ISBN vv-2012-97854674562"};
        }

        public static IEnumerable<object[]> GetIsbn10Code()
        {
            yield return new object[] {"ISBN 1-58182-008-9"};
            yield return new object[] {"ISBN 2-226-05257-7"};
            yield return new object[] {"ISBN 964-6194-70-2"};
            yield return new object[] {"ISBN 965-359-002-2"};
            yield return new object[] {"ISBN 966-95440-5-X"};
            yield return new object[] {"ISBN 972-37-0274-6"};
        }

        public static IEnumerable<object[]> GetIsbn13Code()
        {
            yield return new object[] {"ISBN 978-0-8493-9640-3"};
            yield return new object[] {"ISBN 978-3-16-148410-0"};
            yield return new object[] {"ISBN 978-1-56619-909-4"};
            yield return new object[] {"ISBN 978-1-4028-9462-6"};
            yield return new object[] {"ISBN 978-1-4302-1998-9"};
            yield return new object[] {"ISBN 978-1-86197-876-9"};
        }
    }
}