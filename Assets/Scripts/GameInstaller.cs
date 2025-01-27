using Reflex.Core;
using StreetFight.Obstacle;
using StreetFight.Score;
using UnityEngine;
using UnityEngine.Serialization;

namespace StreetFight
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private LayerMask _obstacleLayerMask;
        [SerializeField] private GameObject _markerPrefab;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddTransient<IGroundService>(container => new GroundService(_obstacleLayerMask, _markerPrefab));
            containerBuilder.AddTransient<IObstacleService>(container => new ObstacleService(_obstacleLayerMask));
            containerBuilder.AddSingleton<IScoreService>(container => new ScoreService());
            containerBuilder.AddSingleton<IObstacleSpawnService>(container => new ObstacleSpawnService());
        }
    }
}
