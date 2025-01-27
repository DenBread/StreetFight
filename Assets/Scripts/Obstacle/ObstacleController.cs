using System;
using Reflex.Attributes;
using StreetFight.Obstacle;
using UnityEngine;

namespace StreetFight
{
    public class ObstacleController : MonoBehaviour
    {
        [Inject] private IGroundService _groundService; // Сервис для проверки столкновения с землёй
        [Inject] public IObstacleService ObstacleService; // Сервис для управления препятствием
        public LayerMask groundLayer; // Слой земли для проверки столкновения

        [Inject]
        public void Construct(IObstacleService obstacleService)
        {
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