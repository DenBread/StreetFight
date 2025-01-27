using System;
using Reflex.Attributes;
using StreetFight.Obstacle;
using UnityEngine;

namespace StreetFight
{
    public class ObstacleController : MonoBehaviour
    {
        private IGroundService _groundService; // Сервис для проверки столкновения с землёй
        public IObstacleService ObstacleService; // Сервис для управления препятствием

        [Inject]
        public void Construct(IGroundService groundService, IObstacleService obstacleService)
        {
            _groundService = groundService; // Инициализация сервиса проверки столкновения с землёй
            ObstacleService = obstacleService; // Инициализация сервиса управления препятствием
            ObstacleService.SetObstacle(this.gameObject); // Установка текущего объекта препятствия
        }

        private void Update()
        {
            _groundService.GroundChecker(transform, transform.position); // Проверка столкновения с землёй
            ObstacleService.CheckGroundAndDestroy(); // Проверка и деактивация препятствия при столкновении с землёй
        }
    }
}