namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public interface ICheckVisible : ISpriteRenderer
    {
        public bool IsVisible { get; set; }
        public void OnInvisible();
    }
}