using CodeBase.StaticData;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameData>().FromResource("StaticData/GameData").AsSingle();
        }
    }
}