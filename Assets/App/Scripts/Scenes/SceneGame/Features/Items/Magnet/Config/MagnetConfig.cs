using System;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.View;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Magnet
{
    [Serializable]
    public class MagnetConfig
    {
        public float MagnetTime = 3f;
        public float MagnetRadius = 5f;
        public float MagnetForce = 3f;
        public MagneticEffectView MagneticEffectView;
    }
}