
using System.Collections.Generic;

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

        public int AchievedBlockLevel;

        public double Money = 0;
        public int BuildBlocksMoney = 0;
        public float MoneyMultiplier = 1;
        public int AdditionalBlockMoney = 0;

        public float SoundVolume = 0.75f;
        public float MusicVolume = 0.75f;

        public List<int> BlocksInCells;
        public float CreationDuration = 3;
        public int CreationBlockLevel = 0;

        public int BlockUpgradeLevel = 1;
        public int CreationSpeedUpgradeLevel = 1;
        public int BlockMoneyUpgradeLevel = 1;
        public int MoneyUpgradeLevel = 1;

        public List<bool> BuildingBlocksActivity;
        public int CreatedBuildingNumber;
        public int DestroyedBuildingsCount;
        public int CreatedBuildingPrefabNumber;

        public bool BuildTutorialFinished = false;
        public bool MainTutorialFinished = false;
    }
}
