using System.Collections;
using Reflex.Attributes;
using StreetFight.Score;
using TMPro;
using UnityEngine;

namespace StreetFight
{
    public class ScoreUIController : MonoBehaviour
    {
        // UI элемент для отображения очков
        [SerializeField] private TextMeshProUGUI _ugui;

        // Сервис для управления очками
        private IScoreService _scoreService;

        // Сервис для управления игроком
        private IPlayerService _playerService;

        // Метод для внедрения зависимостей
        [Inject]
        private void Construct(IScoreService scoreService, IPlayerService playerService)
        {
            _scoreService = scoreService;
            _playerService = playerService;
        }

        private void Start()
        {
            // Подписываемся на событие смерти игрока
            _playerService.OnDeath += HandlePlayerDeath;

            // Подписываемся на события изменения очков и автосчёта
            _scoreService.OnScoreChanged += UpdateScoreUI;
            _scoreService.OnAutoScoringStarted += StartAutoScoring;

            // Сбрасываем очки при старте
            _scoreService.ResetScore();
            // Обновляем текстовое поле при старте
            UpdateScoreUI(_scoreService.GetScore());
        }

        private void OnDestroy()
        {
            // Отписываемся от событий при уничтожении объекта
            if (_playerService != null)
            {
                _playerService.OnDeath -= HandlePlayerDeath;
            }

            if (_scoreService != null)
            {
                _scoreService.OnScoreChanged -= UpdateScoreUI;
                _scoreService.OnAutoScoringStarted -= StartAutoScoring;
            }
        }

        // Метод для обновления UI с текущими очками
        private void UpdateScoreUI(int newScore)
        {
            _ugui.text = newScore.ToString();
        }

        // Метод для запуска автосчёта
        private void StartAutoScoring()
        {
            StartCoroutine(AutoScoringCoroutine());
        }

        // Корутин для автоматического добавления очков
        private IEnumerator AutoScoringCoroutine()
        {
            while (_scoreService.CanAddScore())
            {
                yield return new WaitForSeconds(1);
                _scoreService.AddScore(1);
            }
        }

        // Метод для обработки смерти игрока
        private void HandlePlayerDeath()
        {
            Debug.Log("Player has died. Stopping score updates.");
            _scoreService.DisableScoreAdding();
        }
    }
}