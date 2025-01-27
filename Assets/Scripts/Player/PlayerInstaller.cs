using Reflex.Core;
using UnityEngine;

namespace StreetFight
{
    public class PlayerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Rigidbody _playerRigidBody;
        [SerializeField] private float _movementSpeed = 5f;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton<IPlayerMovementService>(container => new PlayerMovementService(_playerRigidBody, _movementSpeed));
        }
    }
}
