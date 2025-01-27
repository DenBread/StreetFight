using System;
using Reflex.Attributes;
using UnityEngine;

namespace StreetFight
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private IPlayerService _playerService;

        
        private void Start()
        {
            _playerService.OnDeath += HandleDeath;
        }

        private void OnDestroy()
        {
            if (_playerService != null)
            {
                _playerService.OnDeath -= HandleDeath;
            }
        }
        
        private void HandleDeath()
        {
            Debug.Log("Player is dead!");
            // Покажите экран смерти или выполните другую логику
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                _playerService.Die();
            }
        }
    }
}
