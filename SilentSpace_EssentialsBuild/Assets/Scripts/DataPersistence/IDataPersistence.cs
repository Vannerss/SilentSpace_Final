using SilentSpace.DataPersistence.Data;

namespace SilentSpace.DataPersistence
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);

        void SaveData(ref GameData data);
    }
}