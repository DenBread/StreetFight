using UnityEngine;

namespace StreetFight
{
    public class PlayerMovementService : IPlayerMovementService
    {
        private readonly Rigidbody _playerRigidBody;
        private readonly float _movementSpeed;

        public PlayerMovementService(Rigidbody playerRigidBody, float movementSpeed)
        {
            _playerRigidBody = playerRigidBody;
            _movementSpeed = movementSpeed;
        }

        public void Move(Vector3 direction)
        {
            // Если направление равно Vector3.zero, сбрасываем скорость
            if (direction.magnitude > 0.1f)
            {
                _playerRigidBody.linearVelocity = direction * _movementSpeed;
            }
            else
            {
                _playerRigidBody.linearVelocity = Vector3.zero;
            }
        }
    }
}