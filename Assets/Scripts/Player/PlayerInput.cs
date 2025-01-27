using System;
using Reflex.Attributes;
using UnityEngine;

namespace StreetFight
{
    public class PlayerInput : MonoBehaviour
    {
        [Inject] private IPlayerMovementService _movementService; // Сервис для управления движением игрока
        private float _horizontal = 0f; // Горизонтальный ввод
        private float _vertical = 0f; // Вертикальный ввод

        // Метод, вызываемый каждый кадр для получения ввода
        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal"); // Получаем горизонтальный ввод
            _vertical = Input.GetAxisRaw("Vertical"); // Получаем вертикальный ввод
        }

        // Метод, вызываемый на каждом фиксированном кадре для перемещения игрока
        private void FixedUpdate()
        {
            Vector3 direction = new Vector3(_horizontal, 0, _vertical).normalized; // Нормализуем направление движения
            _movementService.Move(direction); // Перемещаем игрока в указанном направлении
        }
    }
}