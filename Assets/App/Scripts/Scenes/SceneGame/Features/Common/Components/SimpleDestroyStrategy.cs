namespace App.Scripts.Scenes.SceneGame.Features.Common.Components
{
    public class SimpleDestroyStrategy : IDestroyStrategy
    {
        public void OnDestroy(ComponentContainer componentContainer)
        {
            componentContainer.Destroy();
        }
    }
}