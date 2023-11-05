namespace EasyPlugins.SaveSystem
{
    public class SaveSystem
    {
        private readonly string PlayerPrefsKey = "SaveData";
    
        public static SaveValues SaveValues;

        private static ISaveLoad _saveLoad;
        
        public SaveSystem()
        {
            _saveLoad = new SaverJson(PlayerPrefsKey);
            Load();
        }

        public static void Save() 
            => _saveLoad.Save(SaveValues);

        private void Load()
        {
            SaveValues = _saveLoad.Load<SaveValues>();

            if (SaveValues == null)
            {
                SaveValues = new SaveValues();
            }
        }
    }
}
