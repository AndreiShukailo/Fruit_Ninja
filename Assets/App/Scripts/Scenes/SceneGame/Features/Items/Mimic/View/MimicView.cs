using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Mimic.View
{
    public class MimicView : UnitView
    {
        [SerializeField] private ParticleSystem _changeMimicEffect;
        [SerializeField] private ParticleSystem _mimicEffect;

        public void PlayChangeMimicEffect() =>
            _changeMimicEffect.Play();

        public void PlayMimicEffect() =>
            _mimicEffect.Play(false);
    }
}