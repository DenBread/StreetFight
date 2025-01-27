using UnityEngine;

namespace StreetFight.Obstacle
{
    // Интерфейс для сервиса управления препятствиями
    public interface IObstacleService
    {
        // Метод для установки текущего объекта препятствия
        void SetObstacle(GameObject obstacle);

        // Метод для активации препятствия
        void ActivateObstacle();

        // Метод для деактивации препятствия
        void DeactivateObstacle();

        // Метод для запуска движения препятствия вниз
        void MoveDown();

        // Метод для проверки столкновения с землёй и деактивации препятствия
        void CheckGroundAndDestroy();
    }
}