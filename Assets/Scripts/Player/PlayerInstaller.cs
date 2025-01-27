using Reflex.Core;
using UnityEngine;

namespace StreetFight
{
    public class PlayerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Rigidbody _playerRigidBody; // Rigidbody игрока для управления физикой
        [SerializeField] private float _movementSpeed = 5f; // Скорость движения игрока
        [SerializeField] private PlayerController _playerController;

        // Метод для установки зависимостей
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            // Регистрируем PlayerMovementService как Singleton
            containerBuilder.AddSingleton<IPlayerMovementService>(container => new PlayerMovementService(_playerRigidBody, _movementSpeed));
            containerBuilder.AddSingleton<IPlayerService>(container => new PlayerService(_playerController.gameObject, _playerController.transform.position));
        }
    }
}