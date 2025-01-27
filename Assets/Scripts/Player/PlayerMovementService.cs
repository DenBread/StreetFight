using UnityEngine;

namespace StreetFight
{
    public class PlayerMovementService : IPlayerMovementService
    {
        private readonly Rigidbody _playerRigidBody; // Rigidbody игрока для управления физикой
        private readonly float _movementSpeed; // Скорость движения игрока

        // Конструктор для инициализации Rigidbody и скорости движения
        public PlayerMovementService(Rigidbody playerRigidBody, float movementSpeed)
        {
            _playerRigidBody = playerRigidBody;
            _movementSpeed = movementSpeed;
        }

        // Метод для перемещения игрока в указанном направлении
        public void Move(Vector3 direction)
        {
            // Если направление не равно Vector3.zero, устанавливаем скорость
            if (direction.magnitude > 0.1f)
            {
                _playerRigidBody.linearVelocity = direction * _movementSpeed;
            }
            else
            {
                _playerRigidBody.linearVelocity =
                    Vector3.zero; // Сбрасываем скорость, если направление равно Vector3.zero
            }
        }
    }
}