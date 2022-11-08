using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuggestionsSystem
{
    public static class FocusHelper
    {
        public static void SetFocus(object targetVM, string propertyToFocus)
        {
            PropertyInfo focusProperty;
            focusProperty = targetVM.GetType().GetProperty(propertyToFocus);
            focusProperty.SetValue(targetVM, true);
        }
    }
}
