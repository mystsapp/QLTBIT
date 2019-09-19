using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            var result = from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
                   };
            return result;

        }

        public static IEnumerable<SelectListItem> ToSelectListItemString<T>(this IEnumerable<T> items, string selectedValue)
        {
            if (selectedValue == null)
                selectedValue = "";

            var result = from item in items
                         select new SelectListItem
                         {
                             Text = item.GetPropertyValue("TenVP"),
                             Value = item.GetPropertyValue("TenVP"),
                             Selected = item.GetPropertyValue("TenVP").Equals(selectedValue.ToString())
                         };

            return result;
        }
    }
}
