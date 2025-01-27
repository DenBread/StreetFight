using System;
using UnityEngine;

namespace StreetFight
{
    public class PlayerService : IPlayerService
    {
        private GameObject _player;
        private int _health = 100; // Начальное здоровье
        private readonly int _maxHealth = 100; // Максимальное здоровье
        private readonly Vector3 _playerPosition;

        public bool IsDead => _health <= 0;
        public int Health => _health;

        public event Action OnDeath;

        public PlayerService(GameObject player, Vector3 playerPosition)
        {
            _player = player;
            _playerPosition = playerPosition;
        }

        public void Restart()
        {
            _player.transform.position = _playerPosition;
            _health = _maxHealth;
            Activate();
        }

        public void Activate()
        {
            _player.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _player.gameObject.SetActive(false);
        }

        public void TakeDamage(int damage)
        {
            if (IsDead) return;

            _health -= damage;
            if (_health <= 0)
            {
                _health = 0;
                TriggerDeath();
            }
        }

        public void Heal(int amount)
        {
            if (IsDead) return;

            _health += amount;
            if (_health > _maxHealth)
            {
                _health = _maxHealth; // Здоровье не может превышать максимум
            }
        }

        public void Die()
        {
            if (IsDead) return;

            _health = 0; // Обнуляем здоровье
            TriggerDeath();
            Deactivate();
        }

        private void TriggerDeath()
        {
            OnDeath?.Invoke();
            Console.WriteLine("Player has died."); // Для отладки
        }
    }
}