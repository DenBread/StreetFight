using System;
        
        namespace StreetFight
        {
            public interface IPlayerService
            {
                // Метод для перезапуска игрока
                void Restart();
        
                // Метод для активации игрока
                void Activate();
        
                // Метод для деактивации игрока
                void Deactivate();
        
                // Метод для нанесения урона игроку
                void TakeDamage(int damage); // Нанести урон
        
                // Метод для лечения игрока
                void Heal(int amount); // Восстановить здоровье
        
                // Свойство, указывающее, мертв ли игрок
                bool IsDead { get; } // Проверить, мёртв ли игрок
        
                // Свойство для получения текущего здоровья
                int Health { get; } // Получить текущее здоровье
        
                // Событие, уведомляющее о смерти игрока
                event Action OnDeath; // Событие смерти
        
                // Метод для убийства игрока
                void Die(); // Принудительно убить
            }
        }