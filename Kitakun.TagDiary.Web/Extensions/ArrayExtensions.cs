namespace Kitakun.TagDiary.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using Kitakun.TagDiary.Core.Extensions;

    public static class ArrayExtensions
    {
        public static List<SelectListItem> PrepareEnum<T>(T selectedValue)
            where T : Enum
         {
            var enumType = typeof(T);
            var enumArray = Enum.GetValues(enumType);
            var result = new List<SelectListItem>(enumArray.Length);
            foreach (var e in enumArray)
            {
                result.Add(new SelectListItem(((T)e).GetAttribute<DisplayAttribute>().Name, e.ToString(), e.Equals(selectedValue)));
            }

            return result;
        }
    }
}
