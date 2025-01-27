using System.Collections;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public interface IObstacleService
    {
        void SetObstacle(GameObject obstacle);
        void ActivateObstacle();
        void DeactivateObstacle();
        void MoveDown();
        void CheckGroundAndDestroy();
    }
}