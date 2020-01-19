namespace Kitakun.TagDiary.Core.Extensions
{
    using System;

    public static class ArrayExtensions
    {
        public static T[] CastArray<F, T>(this F[] srcArray, Func<F, T> method)
        {
            if (srcArray == null || srcArray.Length == 0)
            {
                return default;
            }
            else
            {
                var newArray = new T[srcArray.Length];
                for (var i = 0; i < srcArray.Length; i++)
                {
                    newArray[i] = method(srcArray[i]);
                }
                return newArray;
            }
        }
    }
}
