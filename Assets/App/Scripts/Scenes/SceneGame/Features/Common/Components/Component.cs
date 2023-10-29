namespace App.Scripts.Scenes.SceneGame.Features.Common.Components
{
    public abstract class Component
    {
        public ComponentContainer ComponentContainer;
        
        public virtual void OnDestroy()
        {
            ComponentContainer = null;
        }
    }
}