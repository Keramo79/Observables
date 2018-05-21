using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace M95.Observables
{
    public static class Functions
    {
        public static void Copy<T>(this T source, T dest)
        {
            foreach (PropertyInfo pi in source.GetType().GetProperties())
            {
                if (pi.GetValue(source) != dest.GetType().GetProperty(pi.Name).GetValue(dest))
                {
                    dest.GetType().GetProperty(pi.Name).SetValue(dest, pi.GetValue(source));
                }
            }
        }
    }
}
