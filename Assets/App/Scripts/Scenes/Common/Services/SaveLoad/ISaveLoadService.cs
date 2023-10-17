namespace App.Scripts.Scenes.Common
{
    public interface ISaveLoadService
    {
        public PlayerProgress Load();
        public void Save(PlayerProgress playerProgress);
    }
}