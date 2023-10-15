namespace App.Scripts.Scenes.SceneGame.Features
{
    public class SimpleDestroyStrategy : IDestroyStrategy
    {
        public void OnDestroy(ComponentContainer componentContainer)
        {
            componentContainer.Destroy();
        }
    }
}