namespace CodeBase.Services.Spawner.Zone
{
    public interface ISpawnZone
    {
        public float Priority { get; }
        public void Spawn();
    }
}