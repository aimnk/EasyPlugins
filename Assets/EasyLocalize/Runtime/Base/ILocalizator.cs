namespace EasyLocalize
{
    public interface ILocalizator
    {
        /// <summary>
        /// Get translation by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTranslation(string key);
    }
}
