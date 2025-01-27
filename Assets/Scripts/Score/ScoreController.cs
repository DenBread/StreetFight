using System;
using System.Collections;
using Reflex.Attributes;
using StreetFight.Score;
using TMPro;
using UnityEngine;

namespace StreetFight
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ugui;

        private IScoreService _scoreService;
        private IPlayerService _playerService;

        [Inject]
        private void Construct(IScoreService scoreService, IPlayerService playerService)
        {
            _scoreService = scoreService;
            _playerService = playerService;
        }

        private void Start()
        {
            _playerService.OnDeath += HandlePlayerDeath; // Подписка на событие смерти игрока
            
            // Подписываемся на событие изменения очков
            _scoreService.OnScoreChanged += UpdateScoreUI;
            _scoreService.OnAutoScoringStarted += StartAutoScoring;
            _scoreService.ResetScore();
            // Обновляем текст при старте
            UpdateScoreUI(_scoreService.GetScore());
        }

        private void OnDestroy()
        {
            if (_playerService != null)
            {
                _playerService.OnDeath -= HandlePlayerDeath; // Отписка от события
            }
            
            if (_scoreService != null)
            {
                _scoreService.OnScoreChanged -= UpdateScoreUI;
                _scoreService.OnAutoScoringStarted -= StartAutoScoring;
            }
        }
        
        private void UpdateScoreUI(int newScore)
        {
            _ugui.text = newScore.ToString();
        }
        
        private void StartAutoScoring()
        {
            StartCoroutine(AutoScoringCoroutine());
        }

        private IEnumerator AutoScoringCoroutine()
        {
            while (_scoreService.CanAddScore())
            {
                yield return new WaitForSeconds(1);
                _scoreService.AddScore(1);
            }
        }
        
        private void HandlePlayerDeath()
        {
            Debug.Log("Player has died. Stopping score updates.");
            _scoreService.DisableScoreAdding();
        }
    }
}
