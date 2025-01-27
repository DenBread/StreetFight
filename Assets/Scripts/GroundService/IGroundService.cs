using UnityEngine;

namespace StreetFight
{
    // Интерфейс для сервиса проверки столкновения с землёй
    public interface IGroundService
    {
        // Метод для проверки столкновения с землёй
        void GroundChecker(Transform parent, Vector3 position);

        // Метод для установки маркера на земле
        void MarkGround(Transform parent, Vector3 position);
    }
}