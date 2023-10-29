using App.Scripts.Scenes.SceneGame.Features.Common;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;

namespace App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem
{
    public interface ICheckVisible : ISpriteRenderer
    {
        public ComponentContainer GetContainer();
        public bool IsVisible { get; set; }
        public void OnInvisible();
    }
}