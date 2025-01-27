using System.Collections;
using GameObjectPooling;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public class ObstacleService : IObstacleService
    {
        private GameObject _currentObstacle; // Текущий объект препятствия
        private float _moveSpeed = 5.0f; // Скорость движения препятствия

        private readonly float _rayLength = 2f; // Длина луча для проверки столкновения с землёй
        private readonly LayerMask _groundLayer; // Слой земли для проверки столкновения
        private bool _isActive = false; // Флаг активности препятствия

        // Конструктор для установки слоя земли
        public ObstacleService(LayerMask groundLayer)
        {
            _groundLayer = groundLayer;
        }

        // Метод для установки текущего объекта препятствия
        public void SetObstacle(GameObject obstacle)
        {
            _currentObstacle = obstacle;
        }

        // Метод для проверки наличия Rigidbody у объекта
        public void CheckRigibody()
        {
            // Убедимся, что объект имеет Rigidbody
            if (_currentObstacle != null && _isActive == true)
            {
                if (_currentObstacle.GetComponent<Rigidbody>() == null)
                {
                    var rb = _currentObstacle.AddComponent<Rigidbody>();
                    rb.useGravity = true; // Управляем вручную
                }
            }
        }

        // Метод для запуска движения препятствия вниз
        public void MoveDown()
        {
            CheckRigibody();

            // Запускаем движение вниз
            /*if(_isActive == true)
                _currentObstacle.GetComponent<Rigidbody>().linearVelocity = Vector3.down * _moveSpeed;*/
        }

        // Метод для активации препятствия
        public void ActivateObstacle()
        {
            if (_currentObstacle != null)
            {
                _currentObstacle.gameObject.SetActive(true);
                _isActive = true;
            }
        }

        // Метод для деактивации препятствия
        public void DeactivateObstacle()
        {
            Debug.Log("Deactivate Obstacle");

            if (_currentObstacle != null)
            {
                _isActive = false;
                GameObjectPool.Destroy(_currentObstacle, 1f);
            }
        }

        // Метод для проверки столкновения с землёй и деактивации препятствия
        public void CheckGroundAndDestroy()
        {
            Vector3 origin = _currentObstacle.transform.position;
            Vector3 direction = Vector3.down;

            if(_currentObstacle.transform.position.y < 0)
                DeactivateObstacle();

            if (Physics.Raycast(origin, direction, out RaycastHit hit, _rayLength, _groundLayer) && _isActive == true)
            {
               //Debug.Log($"Ground : {hit.collider.gameObject.name}");
                DeactivateObstacle();
            }
            else
            {
                //Debug.Log("No ground detected.");
            }

            Debug.DrawRay(origin, direction * _rayLength, Color.yellow);
        }
    }
}