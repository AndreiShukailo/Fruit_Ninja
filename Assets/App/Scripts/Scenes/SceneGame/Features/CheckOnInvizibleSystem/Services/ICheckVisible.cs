using App.Scripts.Scenes.SceneGame.Features.Common;

namespace App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services
{
    public interface ICheckVisible : ISpriteRenderer
    {
        public bool IsVisible { get; set; }
        public void OnInvisible();
    }
}