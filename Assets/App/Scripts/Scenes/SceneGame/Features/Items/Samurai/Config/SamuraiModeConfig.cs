using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones.Config;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Services
{
    [Serializable]
    public class SamuraiModeConfig
    {
        public List<SpawnZoneConfig> DisabledSpawnZones;
        public int PackCountFactor = 2;
        public int PackDelayFactor = 2;
        public int TimeOfAction = 10;
        public SamuraiLabelView SamuraiLabelView;
    }
}