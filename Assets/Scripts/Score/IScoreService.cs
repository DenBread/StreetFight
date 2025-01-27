using System;
        using System.Collections;
        using UnityEngine;
        
        namespace StreetFight.Score
        {
            public interface IScoreService
            {
                // Добавляет указанное количество очков
                void AddScore(int score);
                
                // Возвращает текущее количество очков
                int GetScore();
                
                // Проверяет, можно ли добавлять очки
                bool CanAddScore();
                
                // Включает возможность добавления очков
                void EnableScoreAdding();
                
                // Отключает возможность добавления очков
                void DisableScoreAdding();
                
                // Сбрасывает текущие очки
                void ResetScore();
                
                // Сохраняет лучший результат
                void SaveBestScore();
                
                // Возвращает лучший результат
                int GetBestScore();
        
                // Событие, уведомляющее об изменении очков
                event Action<int> OnScoreChanged;
                
                // Событие, запускающее процесс автоматического начисления очков
                event Action OnAutoScoringStarted;
            }
        }