using System;
using App.Scripts.Scenes.SceneGame.Features.Bomb.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Bomb
{
    [Serializable]
    public class BombConfig
    {
        public float ExplosionPower = 3f;
        public float ExplosionRadius;
        public GameObject Explosion;
        public BombPopUpView BombPopUpView;
        public RectTransform BombEffectContainer;
    }
}