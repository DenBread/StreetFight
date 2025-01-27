using Reflex.Attributes;
using UnityEngine;

namespace StreetFight
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private IPlayerService _playerService; // Сервис для управления состоянием игрока

        // Метод, вызываемый при старте
        private void Start()
        {
            _playerService.OnDeath += HandleDeath; // Подписываемся на событие смерти игрока
        }

        // Метод, вызываемый при уничтожении объекта
        private void OnDestroy()
        {
            if (_playerService != null)
            {
                _playerService.OnDeath -= HandleDeath; // Отписываемся от события смерти игрока
            }
        }

        // Метод для обработки смерти игрока
        private void HandleDeath()
        {
            Debug.Log("Player is dead!"); // Логируем сообщение о смерти игрока
            // Покажите экран смерти или выполните другую логику
        }

        // Метод, вызываемый при столкновении с другим объектом
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                _playerService.Die(); // Убиваем игрока при столкновении с препятствием
            }
        }
    }
}