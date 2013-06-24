﻿namespace Coevery.Entities {
    public static class StrinExtensions {
        public static string TrimEnd(this string rough, string trim = "") {
            if (rough == null)
                return null;

            return rough.EndsWith(trim)
                       ? rough.Substring(0, rough.Length - trim.Length)
                       : rough;
        }
    }
}