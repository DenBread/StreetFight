using System;

namespace StreetFight
{
    public interface IPlayerService
    {
        void Restart();
        void Activate();
        void Deactivate();
        void TakeDamage(int damage); // Нанести урон
        void Heal(int amount); // Восстановить здоровье
        bool IsDead { get; } // Проверить, мёртв ли игрок
        int Health { get; } // Получить текущее здоровье
        event Action OnDeath; // Событие смерти
        void Die(); // Принудительно убить
    }
}