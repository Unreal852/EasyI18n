using System;
using System.Collections.Generic;
using System.IO;
using EasyI18n.Events;
using EasyI18n.Locales;

namespace EasyI18n
{
    public static class I18n
    {
        private static Locale s_DefaultLocale;

        private static Dictionary<Locale, Translation> Translations { get; } = new Dictionary<Locale, Translation>();

        /// <summary>
        ///     Default Language
        /// </summary>
        public static Locale DefaultLocale
        {
            get => s_DefaultLocale;
            set
            {
                if(s_DefaultLocale == value)
                    return;
                var e = new DefaultLanguageChangedEventArgs(s_DefaultLocale, value);
                s_DefaultLocale = value;
                OnDefaultLanguageChanged?.Invoke(null, e);
            }
        }

        /// <summary>
        ///     Called when the default language changed
        /// </summary>
        public static event EventHandler<DefaultLanguageChangedEventArgs> OnDefaultLanguageChanged;

        /// <summary>
        ///     Load locales from directory
        /// </summary>
        /// <param name="directory">Locales Directory</param>
        public static void LoadLocales(string directory)
        {
            var dir = new DirectoryInfo(directory);
            if(!dir.Exists)
                return;
            var files = dir.GetFiles("*.xml");
            foreach(var fileInfo in files)
                LoadLocale(fileInfo);
        }

        /// <summary>
        ///     Load translation file
        /// </summary>
        /// <param name="file">Translation File</param>
        public static void LoadLocale(FileInfo file)
        {
            if(!file.Exists)
                return;
            var translation = Translation.FromFile(file);
            if(translation == null)
                return;
            if(Translations.ContainsKey(translation.Locale))
                Translations[translation.Locale].CombineWith(translation);
            else
                Translations.Add(translation.Locale, translation);
        }

        /// <summary>
        ///     Get translation
        /// </summary>
        /// <param name="key">Translation Key</param>
        /// <returns><see cref="string" /> translation if exists, <see cref="string" /> key otherwise</returns>
        public static string Get(string key)
        {
            return Get(DefaultLocale, key);
        }

        /// <summary>
        ///     Get translation
        /// </summary>
        /// <param name="key">Translation Key</param>
        /// <param name="args">Translation Args</param>
        /// <returns><see cref="string" /> translation if exists, <see cref="string" /> key otherwise</returns>
        public static string Get(string key, params object[] args)
        {
            return Get(DefaultLocale, key, args);
        }

        /// <summary>
        ///     Get translation
        /// </summary>
        /// <param name="locale">Locale</param>
        /// <param name="key">Translation Key</param>
        /// <param name="args">Arguments to replace</param>
        /// <returns><see cref="string" /> translation, <see cref="string" />Key otherwise</returns>
        public static string Get(Locale locale, string key, params object[] args)
        {
            if(!Translations.ContainsKey(locale) || !Translations[locale].ContainsKey(key))
                return key;
            if(args == null || args.Length <= 0)
                return Translations[locale][key];
            var translated = Translations[locale][key];
            for(var i = 0; translated.Contains($"[{i}]"); i++)
            {
                if(args.Length - 1 < i)
                    break;
                var currentArg = args[i];
                if(currentArg is KeyValue value)
                {
                    if(Translations[locale].ContainsKey(value.Key))
                        translated = translated.Replace($"[{i}]", Get(locale, Translations[locale][value.Key], null));
                    continue;
                }

                translated = translated.Replace($"[{i}]", currentArg != null ? currentArg.ToString() : "");
            }

            return translated;
        }
    }
}