using System;
using UnityEngine;

namespace StreetFight
{
    public class PlayerService : IPlayerService
    {
        private GameObject _player; // Игровой объект игрока
        private int _health = 100; // Начальное здоровье
        private readonly int _maxHealth = 100; // Максимальное здоровье
        private readonly Vector3 _playerPosition; // Начальная позиция игрока

        // Свойство, указывающее, мертв ли игрок
        public bool IsDead => _health <= 0;

        // Свойство для получения текущего здоровья
        public int Health => _health;

        // Событие, уведомляющее о смерти игрока
        public event Action OnDeath;

        // Конструктор для инициализации игрока и его позиции
        public PlayerService(GameObject player, Vector3 playerPosition)
        {
            _player = player;
            _playerPosition = playerPosition;
        }

        // Метод для перезапуска игрока
        public void Restart()
        {
            _player.transform.position = _playerPosition; // Возвращаем игрока на начальную позицию
            _health = _maxHealth; // Восстанавливаем здоровье
            Activate(); // Активируем игрока
        }

        // Метод для активации игрока
        public void Activate()
        {
            _player.gameObject.SetActive(true);
        }

        // Метод для деактивации игрока
        public void Deactivate()
        {
            _player.gameObject.SetActive(false);
        }

        // Метод для нанесения урона игроку
        public void TakeDamage(int damage)
        {
            if (IsDead) return;

            _health -= damage; // Уменьшаем здоровье
            if (_health <= 0)
            {
                _health = 0;
                TriggerDeath(); // Вызываем событие смерти
            }
        }

        // Метод для лечения игрока
        public void Heal(int amount)
        {
            if (IsDead) return;

            _health += amount; // Увеличиваем здоровье
            if (_health > _maxHealth)
            {
                _health = _maxHealth; // Здоровье не может превышать максимум
            }
        }

        // Метод для убийства игрока
        public void Die()
        {
            if (IsDead) return;

            _health = 0; // Обнуляем здоровье
            TriggerDeath(); // Вызываем событие смерти
            Deactivate(); // Деактивируем игрока
        }

        // Метод для вызова события смерти
        private void TriggerDeath()
        {
            OnDeath?.Invoke(); // Уведомляем подписчиков о смерти
            Console.WriteLine("Player has died."); // Для отладки
        }
    }
}