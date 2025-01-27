using DG.Tweening;
using Reflex.Attributes;
using StreetFight.Obstacle;
using StreetFight.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StreetFight
{
    public class GameOverController : MonoBehaviour
    {
        // Панель, отображающаяся при смерти
        [SerializeField] private CanvasGroup _deathPanel;

        // Кнопка для перезапуска игры
        [SerializeField] private Button _restartButton;

        // Текст для лучшего счёта
        [SerializeField] private TextMeshProUGUI _bestScoreText;

        // Текст для текущего счёта
        [SerializeField] private TextMeshProUGUI _yourScoreText;

        // Сервис для управления игроком
        private IPlayerService _playerService;

        // Сервис для управления счётом
        private IScoreService _scoreService;

        // Сервис для управления спавном препятствий
        private IObstacleSpawnService _obstacleSpawnService;

        // Метод для внедрения зависимостей
        [Inject]
        private void Construct(IPlayerService playerService, IScoreService scoreService,
            IObstacleSpawnService obstacleSpawnService)
        {
            _playerService = playerService;
            _scoreService = scoreService;
            _obstacleSpawnService = obstacleSpawnService;

            // Подписываемся на событие смерти игрока
            _playerService.OnDeath += ShowDeathPanel;
        }

        private void Start()
        {
            // Добавляем обработчик нажатия на кнопку перезапуска
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            // Отписываемся от события смерти игрока при уничтожении объекта
            if (_playerService != null)
            {
                _playerService.OnDeath -= ShowDeathPanel;
            }
        }

        // Метод для отображения панели смерти
        private void ShowDeathPanel()
        {
            // Сохраняем лучший результат
            _scoreService.SaveBestScore();

            // Обновляем текстовые поля
            _bestScoreText.text = $"BEST SCORE: {_scoreService.GetBestScore()}";
            _yourScoreText.text = $"YOUR SCORE: {_scoreService.GetScore()}";

            // Плавно отображаем панель смерти
            _deathPanel.DOFade(1, 0.5f);
        }

        // Метод для скрытия панели смерти
        private void HideDeathPanel()
        {
            // Плавно скрываем панель смерти
            _deathPanel.DOFade(0, 0.5f);
        }

        // Метод для перезапуска игры
        private void Restart()
        {
            // Перезапускаем игрока
            _playerService.Restart();
            // Сбрасываем счёт
            _scoreService.ResetScore();
            // Перезапускаем спавн препятствий
            _obstacleSpawnService.RestartSpawning();

            // Скрываем панель смерти
            HideDeathPanel();
        }
    }
}