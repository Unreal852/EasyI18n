using System.Collections.Generic;
using System.IO;
using System.Xml;
using EasyI18n.Locales;

namespace EasyI18n
{
    public class Translation
    {
        /// <summary>
        /// Load translation from file
        /// </summary>
        /// <param name="filePath">Translation File</param>
        /// <returns><see cref="Translation" /> if file sucessfully loaded, <see cref="null" /> otherwise</returns>
        public static Translation FromFile(FileInfo file)
        {
            if(!file.Exists || !file.FullName.EndsWith(".xml"))
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(file.FullName);
            if(doc.DocumentElement == null || !doc.DocumentElement.HasAttribute("Locale"))
                return null;
            Locale locale = LocaleHelper.GetLocaleFromString(doc.DocumentElement.GetAttribute("Locale"));
            if(locale == Locale.unk_UNK)
                return null;
            string localeName = doc.DocumentElement.GetAttribute("Name");
            Dictionary<string, string> translations = new Dictionary<string, string>();
            foreach(XmlElement element in doc.DocumentElement.ChildNodes)
                if(element.Name.ToLower() == "category")
                {
                    foreach(XmlElement subElement in element.ChildNodes)
                    {
                        if(!subElement.HasAttribute("Key") || !subElement.HasAttribute("Value"))
                            continue;
                        translations.Add(subElement.GetAttribute("Key"), subElement.GetAttribute("Value"));
                    }
                }
                else
                {
                    if(!element.HasAttribute("Key") || !element.HasAttribute("Value"))
                        continue;
                    translations.Add(element.GetAttribute("Key"), element.GetAttribute("Value"));
                }

            return new Translation(locale, localeName, translations);
        }
        
        public Translation(Locale locale, string name, Dictionary<string, string> translations)
        {
            Locale = locale;
            Name = name;
            Translations = translations ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Locale
        /// </summary>
        public Locale Locale { get; }

        /// <summary>
        /// Locale Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Stored Translations
        /// </summary>
        public Dictionary<string, string> Translations { get; }

        /// <summary>
        /// Get Translation
        /// </summary>
        /// <param name="key">Key</param>
        public string this[string key]
        {
            get
            {
                if(ContainsKey(key))
                    return Translations[key];
                return null;
            }
        }

        /// <summary>
        /// Add a new translation
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Translation Value</param>
        /// <param name="overrideIfExists">Override existing key</param>
        public void Add(string key, string value, bool overrideIfExists = false)
        {
            if(Translations.ContainsKey(key) && !overrideIfExists)
                return;
            Translations.Add(key, value);
        }

        /// <summary>
        /// Checks if the specified key exists
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns><see cref="true" /> is key exists, <see cref="false" /> otherwise</returns>
        public bool ContainsKey(string key)
        {
            return Translations.ContainsKey(key);
        }

        /// <summary>
        /// Add all translations from the specified one into this
        /// </summary>
        /// <param name="translation">Translation to combine</param>
        public void CombineWith(Translation translation)
        {
            foreach(KeyValuePair<string, string> kv in translation.Translations)
                Add(kv.Key, kv.Value);
        }
    }
}