using System;
using DG.Tweening;
using Reflex.Attributes;
using StreetFight.Obstacle;
using StreetFight.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StreetFight
{
    public class DeathController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _deathPanel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _bestScoreText; // Текст для лучшего счёта
        [SerializeField] private TextMeshProUGUI _yourScoreText; // Текст для текущего счёта

        private IPlayerService _playerService;
        private IScoreService _scoreService;
        private IObstacleSpawnService _obstacleSpawnService;

        [Inject]
        private void Construct(IPlayerService playerService, IScoreService scoreService, IObstacleSpawnService obstacleSpawnService)
        {
            _playerService = playerService;
            _scoreService = scoreService;
            _obstacleSpawnService = obstacleSpawnService;
            
            _playerService.OnDeath += ShowDeathPanel;
            
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            if (_playerService != null)
            {
                _playerService.OnDeath -= ShowDeathPanel;
            }
        }

        private void ShowDeathPanel()
        {
            // Сохраняем лучший результат
            _scoreService.SaveBestScore();

            // Обновляем текстовые поля
            _bestScoreText.text = $"BEST SCORE: {_scoreService.GetBestScore()}";
            _yourScoreText.text = $"YOUR SCORE: {_scoreService.GetScore()}";
            
            _deathPanel.DOFade(1, 0.5f);
        }

        private void HideDeathPanel()
        {
            _deathPanel.DOFade(0, 0.5f);
        }
        
        private void Restart()
        {
            _playerService.Restart();
            _scoreService.ResetScore();
            _obstacleSpawnService.RestartSpawning();

            HideDeathPanel();
        }
    }
}