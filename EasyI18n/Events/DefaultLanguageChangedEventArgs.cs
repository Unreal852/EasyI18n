using System;
using EasyI18n.Locales;

namespace EasyI18n.Events
{
    public class DefaultLanguageChangedEventArgs : EventArgs
    {
        public DefaultLanguageChangedEventArgs(Locale oldLocale, Locale newLocale)
        {
            Old = oldLocale;
            New = newLocale;
        }

        /// <summary>
        ///     Old Locale
        /// </summary>
        public Locale Old { get; }

        /// <summary>
        ///     New Locale
        /// </summary>
        public Locale New { get; }
    }
}