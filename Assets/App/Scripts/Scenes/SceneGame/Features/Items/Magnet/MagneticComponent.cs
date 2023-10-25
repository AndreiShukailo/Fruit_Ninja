using UnityEngine;
using Component = App.Scripts.Scenes.SceneGame.Features.Common.Components.Component;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Magnet
{
    public class MagneticComponent : Component
    {
        public bool IsMagnetic { get; private set; }
        public float StartMass { get; private set; }
        public Vector3 EndPoint { get; private set; }
        public float MagneticTimer { get; set; }

        public MagneticComponent(float startMass)
        {
            StartMass = startMass;
        }

        public void SetMagnetic(Vector3 endPoint, float magneticTime)
        {
            IsMagnetic = true;
            EndPoint = endPoint;
            MagneticTimer = magneticTime;
        }

        public void SetUnMagnetic() => IsMagnetic = false;
    }
}