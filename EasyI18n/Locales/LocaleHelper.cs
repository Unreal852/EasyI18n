using System;
using System.Collections.Generic;

namespace EasyI18n.Locales
{
    public static class LocaleHelper
    {
        private static Dictionary<Locale, string> Locales { get; } = new Dictionary<Locale, string>
        {
            {Locale.unk_UNK, "Unknow (Unknow)"},
            {Locale.fr_FR, "French (France)"},
            {Locale.ro_RO, "Romanian (Romania)"},
            {Locale.pt_BR, "Portuguese (Brazil)"},
            {Locale.he_IL, "Hebrew (Israel)"},
            {Locale.de_DE, "German (Germany)"},
            {Locale.it_IT, "Italian (Italy)"},
            {Locale.ru_RU, "Russian (Russia)"},
            {Locale.nl_NL, "Dutch (Netherlands)"},
            {Locale.hu_HU, "Hungarian (Hungary)"},
            {Locale.fa_IR, "Persian (Iran)"},
            {Locale.pl_PL, "Polish (Poland)"},
            {Locale.sk_SK, "Slovak (Slovakia)"},
            {Locale.es_ES, "Spanish (Spain)"},
            {Locale.sv_SE, "Swedish (Sweden)"},
            {Locale.bs_BA, "Bosnian (Bosnia and Herzegovina)"},
            {Locale.bg_BG, "Bulgarian (Bulgaria)"},
            {Locale.zh_CN, "Chinese (China)"},
            {Locale.cs_CZ, "Czech (Czech Republic)"},
            {Locale.da_DK, "Danish (Denmark)"},
            {Locale.gl_ES, "Galician (Spain)"},
            {Locale.is_IS, "Icelandic (Iceland)"},
            {Locale.id_ID, "Indonesian (Indonesia)"},
            {Locale.ko_KR, "Korean (Korea)"},
            {Locale.lt_LT, "Lithuanian (Lithuania)"},
            {Locale.nb_NO, "Norwegian Bokmål (Norway)"},
            {Locale.nn_NO, "Norwegian Nynorsk (Norway)"},
            {Locale.pt_PT, "Portuguese (Portugal)"},
            {Locale.sr_RS, "Serbian (Serbia)"},
            {Locale.sl_SI, "Slovenian (Slovenia)"},
            {Locale.es_PE, "Spanish (Peru)"},
            {Locale.ta_LK, "Tamil (Sri-Lanka)"},
            {Locale.tr_TR, "Turkish (Turkey)"},
            {Locale.my_MM, "Burmese (Myanmar)"},
            {Locale.zh_TW, "Chinese (Taiwan)"},
            {Locale.es_CL, "Spanish (Chile)"},
            {Locale.en_GB, "English (United Kingdom)"},
            {Locale.en_US, "English (United States)"},
            {Locale.bn_BD, "Bengali (Bangladesh)"},
            {Locale.es_MX, "Spanish (Mexico)"},
            {Locale.es_VE, "Spanish (Venezuela)"},
            {Locale.be_BY, "Belarusian (Belarus)"},
            {Locale.de_AT, "German (Austria)"},
            {Locale.gu_IN, "Gujarati (India)"},
            {Locale.hi_IN, "Hindi (India)"},
            {Locale.mk_MK, "Macedonian (Macedonia)"},
            {Locale.ne_NP, "Nepali (Nepal)"},
            {Locale.si_LK, "Sinhala (Sri Lanka)"},
            {Locale.tk_TM, "Turkmen (Turkmenistan)"},
            {Locale.ur_PK, "Urdu (Pakistan)"}
        };

        /// <summary>
        ///     Get the name of the specified locale
        /// </summary>
        /// <param name="locale">Locale</param>
        /// <returns><see cref="string" /> Locale name, <see cref="Locale.unk_UNK" /> otherwise</returns>
        public static string GetLocaleName(this Locale locale)
        {
            if(Locales.ContainsKey(locale))
                return Locales[locale];
            return Locales[Locale.unk_UNK];
        }

        /// <summary>
        ///     Get locale from name
        /// </summary>
        /// <param name="localeName">Locale Name</param>
        /// <returns><see cref="Locale" /> if name matched, <see cref="Locale.unk_UNK" /> otherwise</returns>
        public static Locale GetLocaleByName(string localeName)
        {
            foreach(var value in Locales)
                if(value.Value.ToLower().Contains(localeName.ToLower()))
                    return value.Key;
            return Locale.unk_UNK;
        }

        /// <summary>
        ///     Get locale from string
        /// </summary>
        /// <param name="locale">Locale</param>
        /// <returns><see cref="Locale" /> if the given string is valid, <see cref="Locale.unk_UNK" /> otherwise</returns>
        public static Locale GetLocaleFromString(string locale)
        {
            if(Enum.TryParse(locale, out Locale loc))
                return loc;
            return Locale.unk_UNK;
        }
    }
}