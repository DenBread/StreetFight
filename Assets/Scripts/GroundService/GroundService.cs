using GameObjectPooling;
using Reflex.Injectors;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public class GroundService : IGroundService
    {
        private readonly float _rayLength = 20.0f;
        private readonly LayerMask _groundLayer;
        private readonly GameObject _markerPrefab;
        private GameObject _currentMarker;
        private Transform _parentTransform;

        public GroundService(LayerMask groundLayer)
        {
            _groundLayer = groundLayer;
            _markerPrefab = Resources.Load<GameObject>("GoalObstacle/ShowPointObstacle");
        }

        public void GroundChecker(Transform parent, Vector3 position)
        {
            _parentTransform = parent;
            Vector3 origin = position;
            Vector3 direction = Vector3.down;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, _rayLength, _groundLayer))
            {
                //Debug.Log($"Ground detected: {hit.collider.gameObject.name}");
                MarkGround(_parentTransform, hit.point);
            }
            else
            {
                //Debug.Log("No ground detected.");
                
                /*if(parent == null)
                    GameObjectPool.Destroy(_currentMarker);*/
            }

            Debug.DrawRay(origin, direction * _rayLength, Color.red);
        }

        public void MarkGround(Transform parent, Vector3 position)
        {
            Vector3 adjustedPosition = position + new Vector3(0, 0.05f, 0);
            Quaternion rotation = Quaternion.Euler(90, 0, 0);

            if (_currentMarker == null)
            {
                _currentMarker = GameObjectPool.Instantiate(_markerPrefab, adjustedPosition, rotation, parent);
            }
            else
            {
                _currentMarker.transform.position = adjustedPosition;
                _currentMarker.transform.rotation = rotation;
            }
        }
    }
}