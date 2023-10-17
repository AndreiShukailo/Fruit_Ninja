using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.ComboSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.ComboSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.ComboSystem.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class ComboSystemInstaller : MonoInstaller
    {
        [SerializeField] private ComboPopupView _comboPopupView;
        [SerializeField] private ComboConfig _comboConfig;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetITickableAndSelf(new ComboSystem(_comboConfig, _comboPopupView));
        }
    }
}