namespace App.Scripts.Scenes.Common.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        public PlayerProgress Load();
        public void Save(PlayerProgress playerProgress);
    }
}