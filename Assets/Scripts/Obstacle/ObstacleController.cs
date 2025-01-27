using System;
using Reflex.Attributes;
using StreetFight.Obstacle;
using UnityEngine;

namespace StreetFight
{
    public class ObstacleController : MonoBehaviour
    {
        [Inject] private IGroundService _groundService;
        [Inject] public IObstacleService ObstacleService;
        public LayerMask groundLayer;

        [Inject]
        public void Construct(IObstacleService obstacleService)
        {
            ObstacleService = obstacleService;
            ObstacleService.SetObstacle(this.gameObject);
        }
        

        private void Update()
        {
            _groundService.GroundChecker(transform, transform.position);
            ObstacleService.CheckGroundAndDestroy();
            
        }
        
        
    }
}
