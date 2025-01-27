// Copyright (c) 2024 Pavel Teslenko
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using UnityEngine;
using UnityEngine.Internal;

namespace GameObjectPooling
{
    public static class GameObjectPool
    {
        static private GameObjectPoolInstance _gameObjectPoolInstance;
        static private GameObjectPoolInstance GameObjectPoolInstance
        {
            get
            {
                if(_gameObjectPoolInstance == null)
                {
                    GameObject gameObjectPool = new GameObject("GameObjectPool");
                    _gameObjectPoolInstance = gameObjectPool.AddComponent<GameObjectPoolInstance>();
                }
                return _gameObjectPoolInstance;
            }
        }

        public static T Instantiate<T>(T original) where T : Component
        {
            return GameObjectPoolInstance.Instantiate(original);
        }

        public static GameObject Instantiate(GameObject original, Vector3 position, 
            Quaternion rotation)
        {
            return GameObjectPoolInstance.Instantiate(original, position, rotation);
        }

        public static GameObject Instantiate(GameObject original, Vector3 position, 
            Quaternion rotation, Transform parent)
        {
            return GameObjectPoolInstance.Instantiate(original, position, rotation, parent);
        }

        public static GameObject Instantiate(GameObject original)
        {
            return GameObjectPoolInstance.Instantiate(original);
        }

        public static GameObject Instantiate(GameObject original, Transform parent)
        {
            return GameObjectPoolInstance.Instantiate(original, parent);
        }

        public static T Instantiate<T>(T original, Transform parent, 
            bool worldPositionStays) where T : Component
        {
            return GameObjectPoolInstance.Instantiate(original, parent, worldPositionStays);
        }

        public static T Instantiate<T>(T original, Transform parent) where T : Component
        {
            return GameObjectPoolInstance.Instantiate(original, parent);
        }

        public static T Instantiate<T>(T original, Vector3 position, 
            Quaternion rotation, Transform parent) where T : Component
        {
            return GameObjectPoolInstance.Instantiate(original, position, rotation, parent);
        }

        public static T Instantiate<T>(T original, Vector3 position, 
            Quaternion rotation) where T : Component
        {
            return GameObjectPoolInstance.Instantiate(original, position, rotation);
        }

        public static GameObject Instantiate(GameObject original, Transform parent, 
            bool instantiateInWorldSpace)
        {
            return GameObjectPoolInstance.Instantiate(original, parent, instantiateInWorldSpace);
        }

        public static void Destroy(GameObject obj)
        {
            GameObjectPoolInstance.Destroy(obj);
        }

        public static void Destroy(GameObject obj, [DefaultValue("0.0F")] float t)
        {
            GameObjectPoolInstance.Destroy(obj, t);
        }
    }
}
