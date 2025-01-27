using System;
using System.Collections;
using UnityEngine;

namespace StreetFight.Score
{
    public class ScoreService : IScoreService
    {
        private const string BestScoreKey = "BestScore"; // Ключ для хранения лучшего результата
        
        private int _score;
        private bool _canAddScore = true; // По умолчанию можно добавлять очки

        public event Action<int> OnScoreChanged; // Событие для уведомления об изменении очков
        public event Action OnAutoScoringStarted;
    

        public void AddScore(int score)
        {
            if (_canAddScore)
            {
                _score += score;
                OnScoreChanged?.Invoke(_score); // Уведомляем об изменении
            }
        }

        public int GetScore()
        {
            return _score;
        }

        public bool CanAddScore()
        {
            return _canAddScore;
        }
        
        public int GetBestScore() => PlayerPrefs.GetInt(BestScoreKey, 0); // Получаем лучший результат
        
        public void SaveBestScore()
        {
            if (_score > GetBestScore())
            {
                PlayerPrefs.SetInt(BestScoreKey, _score); // Сохраняем, если текущий счёт больше
            }
        }

        public void EnableScoreAdding()
        {
            _canAddScore = true;
        }

        public void DisableScoreAdding()
        {
            _canAddScore = false;
        }

        public void ResetScore()
        {
            _score = 0;
            EnableScoreAdding();
            OnScoreChanged?.Invoke(_score); // Уведомляем об изменении
            OnAutoScoringStarted?.Invoke(); // Уведомляем о необходимости начать автоматическое начисление
        }
    }
}