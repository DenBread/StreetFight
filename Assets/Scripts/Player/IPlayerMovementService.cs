using UnityEngine;

namespace StreetFight
{
    // Интерфейс для сервиса управления движением игрока
    public interface IPlayerMovementService
    {
        // Метод для перемещения игрока в указанном направлении
        void Move(Vector3 direction);
    }
}