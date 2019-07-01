namespace EasyI18n
{
    public class KeyValue
    {
        public KeyValue(string key)
        {
            Key = key;
        }

        public string Key { get; }

        /// <summary>
        ///     Return key
        /// </summary>
        /// <returns><see cref="string" /> Key</returns>
        public override string ToString()
        {
            return Key;
        }
    }
}