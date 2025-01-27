using System;
using System.Collections;
using UnityEngine;

namespace StreetFight.Score
{
    public class ScoreService : IScoreService
    {
        private const string BestScoreKey = "BestScore"; // Ключ для хранения лучшего результата

        private int _score; // Текущие очки
        private bool _canAddScore = true; // Флаг, указывающий, можно ли добавлять очки

        // Событие для уведомления об изменении очков
        public event Action<int> OnScoreChanged;

        // Событие для запуска процесса автоматического начисления очков
        public event Action OnAutoScoringStarted;

        // Добавляет указанное количество очков
        public void AddScore(int score)
        {
            if (_canAddScore)
            {
                _score += score;
                OnScoreChanged?.Invoke(_score); // Уведомляем об изменении очков
            }
        }

        // Возвращает текущее количество очков
        public int GetScore()
        {
            return _score;
        }

        // Проверяет, можно ли добавлять очки
        public bool CanAddScore()
        {
            return _canAddScore;
        }

        // Возвращает лучший результат
        public int GetBestScore() => PlayerPrefs.GetInt(BestScoreKey, 0);

        // Сохраняет лучший результат, если текущие очки выше
        public void SaveBestScore()
        {
            if (_score > GetBestScore())
            {
                PlayerPrefs.SetInt(BestScoreKey, _score);
            }
        }

        // Включает возможность добавления очков
        public void EnableScoreAdding()
        {
            _canAddScore = true;
        }

        // Отключает возможность добавления очков
        public void DisableScoreAdding()
        {
            _canAddScore = false;
        }

        // Сбрасывает текущие очки и уведомляет об изменении
        public void ResetScore()
        {
            _score = 0;
            EnableScoreAdding();
            OnScoreChanged?.Invoke(_score); // Уведомляем об изменении очков
            OnAutoScoringStarted?.Invoke(); // Уведомляем о начале автоматического начисления очков
        }
    }
}