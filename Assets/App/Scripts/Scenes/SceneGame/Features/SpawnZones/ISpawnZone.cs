namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones
{
    public interface ISpawnZone
    {
        public float Priority { get; }
        public void Spawn();
    }
}