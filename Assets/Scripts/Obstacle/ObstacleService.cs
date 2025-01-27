using System.Collections;
using GameObjectPooling;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public class ObstacleService : IObstacleService
    {
        private GameObject _currentObstacle;
        private float _moveSpeed = 5.0f;
        
        private readonly float _rayLength = 2f;
        private readonly LayerMask _groundLayer;
        private bool _isActive = false;

        public ObstacleService(LayerMask groundLayer)
        {
            _groundLayer = groundLayer;
        }

        public void SetObstacle(GameObject obstacle)
        {
            _currentObstacle = obstacle;
        }

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

        public void MoveDown()
        {
            CheckRigibody();
            
            // Запускаем движение вниз
            /*if(_isActive == true)
                _currentObstacle.GetComponent<Rigidbody>().linearVelocity = Vector3.down * _moveSpeed;*/
        }

        public void ActivateObstacle()
        {
            if (_currentObstacle != null)
            {
                _currentObstacle.gameObject.SetActive(true);
                _isActive = true;
            }
        }
        

        public void DeactivateObstacle()
        {
            Debug.Log("Deactivate Obstacle");
            
            if (_currentObstacle != null)
            {
                _isActive = false;
                GameObjectPool.Destroy(_currentObstacle, 1f);
            }
            
        }

        // Проверяем столкновение с землёй
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