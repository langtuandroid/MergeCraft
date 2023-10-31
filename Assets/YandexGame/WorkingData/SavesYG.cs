
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public double Money = 0;
        public int BuildBlocksMoney = 0;
        public float MoneyMultiplier = 1;
        public int AdditionalBlockMoney = 0;

        public float SoundVolume = 0;
        public float MusicVolume = 0;

        public Cell[] Cells;
        public float CreationDuration = 3;
        public int CreationBlockLevel = 0;

        public BlockLevelUpgrade BlockLevelUpgrade;
        public CreationSpeedUpgrade CreationSpeedUpgrade;
        public BlockMoneyUpgrade BlockMoneyUpgrade;
        public MoneyUpgrade MoneyUpgrade;

        public int CreatedBuildingNumber = 1;
        public Building CreatedBuilding;

        public bool BuildTutorialFinished;
        public bool MainTutorialFinished;
    }
}
