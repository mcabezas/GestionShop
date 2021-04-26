using System.Linq;

namespace Libs.Common
{
    public class Arrays
    {
        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            return !array1.Where((t, i) => t != array2[i]).Any();
        }
    }
}