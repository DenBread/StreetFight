using GameObjectPooling;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public class GroundService : IGroundService
    {
        private readonly float _rayLength = 20.0f; // Длина луча для проверки столкновения с землёй
        private readonly LayerMask _groundLayer; // Слой земли для проверки столкновения
        private readonly GameObject _markerPrefab; // Префаб маркера для обозначения точки на земле
        private GameObject _currentMarker; // Текущий маркер
        private Transform _parentTransform; // Родительский трансформ для маркера

        // Конструктор для установки слоя земли и загрузки префаба маркера
        public GroundService(LayerMask groundLayer, GameObject markerPrefab)
        {
            _groundLayer = groundLayer;
            
            if(markerPrefab != null)
                _markerPrefab = markerPrefab;
            else
                _markerPrefab = Resources.Load<GameObject>("GoalObstacle/ShowPointObstacle");
        }

        // Метод для проверки столкновения с землёй
        public void GroundChecker(Transform parent, Vector3 position)
        {
            _parentTransform = parent;
            Vector3 origin = position;
            Vector3 direction = Vector3.down;

            // Проверка столкновения с землёй с помощью луча
            if (Physics.Raycast(origin, direction, out RaycastHit hit, _rayLength, _groundLayer))
            {
                // Если земля обнаружена, пометить точку на земле
                MarkGround(_parentTransform, hit.point);
            }
            else
            {
                // Если земля не обнаружена, можно добавить логику для уничтожения маркера
            }

            // Визуализация луча для отладки
            Debug.DrawRay(origin, direction * _rayLength, Color.red);
        }

        // Метод для установки маркера на земле
        public void MarkGround(Transform parent, Vector3 position)
        {
            Vector3 adjustedPosition = position + new Vector3(0, 0.05f, 0); // Корректировка позиции маркера
            Quaternion rotation = Quaternion.Euler(90, 0, 0); // Установка вращения маркера

            // Если маркер ещё не создан, создать новый маркер
            if (_currentMarker == null)
            {
                _currentMarker = GameObjectPool.Instantiate(_markerPrefab, adjustedPosition, rotation, parent);
            }
            else
            {
                // Если маркер уже существует, обновить его позицию и вращение
                _currentMarker.transform.position = adjustedPosition;
                _currentMarker.transform.rotation = rotation;
            }
        }
    }
}