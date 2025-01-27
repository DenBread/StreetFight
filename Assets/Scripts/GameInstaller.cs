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
        
        [SerializeField] private PlayerController _playerController;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            /*builder.Bind<ObjectPool<MyPooledObject>>().ToInstance(new ObjectPool<MyPooledObject>(
                factory: () => new MyPooledObject(),
                initialSize: 10
            ));*/
            
            containerBuilder.AddTransient<IGroundService>(container => new GroundService(_obstacleLayerMask));
            containerBuilder.AddTransient<IObstacleService>(container => new ObstacleService(_obstacleLayerMask));
            containerBuilder.AddSingleton<IScoreService>(container => new ScoreService());
            containerBuilder.AddSingleton<IPlayerService>(container => new PlayerService(_playerController.gameObject, _playerController.transform.position));
            containerBuilder.AddSingleton<IObstacleSpawnService>(container => new ObstacleSpawnService());
        }
    }
}
