using UnityEngine;

namespace StreetFight
{
    public interface IGroundService
    {
        void GroundChecker(Transform parent, Vector3 position);
        void MarkGround(Transform parent, Vector3 position);
    }
}