using System;
using System.Collections;
using UnityEngine;

namespace StreetFight.Score
{
    public interface IScoreService
    {
        void AddScore(int score);
        int GetScore();
        bool CanAddScore();
        void EnableScoreAdding();
        void DisableScoreAdding();
        void ResetScore();
        void SaveBestScore();
        int GetBestScore();
        
        event Action<int> OnScoreChanged; // Уведомляет об изменении очков
        event Action OnAutoScoringStarted; // Запускает процесс автоматического начисления очков
    }
}