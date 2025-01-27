using System;
using Reflex.Attributes;
using UnityEngine;

namespace StreetFight
{
    public class PlayerInput : MonoBehaviour
    {
        [Inject] private IPlayerMovementService _movementService;
        private float _horizontal = 0f;
        private float _vertical = 0f;

        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            Vector3 direction = new Vector3(_horizontal, 0, _vertical).normalized;
            _movementService.Move(direction);
        }
    }
}
