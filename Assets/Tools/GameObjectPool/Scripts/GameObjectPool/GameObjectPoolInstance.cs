// Copyright (c) 2024 Pavel Teslenko
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjectPooling
{
    public class GameObjectPoolInstance : MonoBehaviour
    {

        private Dictionary<Object, (Stack<Object>, DefaultGameObjectValues)> _pools = new();
        private Dictionary<GameObject, (Object, Stack<Object>)> _usedObjects = new();

        public GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, 
            Transform parent = null)
        {
            GameObject reusedGameObject;

            if (_pools.TryGetValue(original, 
                out (Stack<Object> pool, DefaultGameObjectValues defaultValues) pool_values))
            {
                Object reusedObject = null;
                while (pool_values.pool.TryPop(out reusedObject) && reusedObject == null) { };

                if(reusedObject != null)
                {
                    reusedGameObject = reusedObject as GameObject;
                    reusedGameObject.transform.parent = parent;
                    reusedGameObject.transform.position = position;
                    reusedGameObject.transform.rotation = rotation;
                    reusedGameObject.transform.localScale = pool_values.defaultValues.Scale;
                    reusedGameObject.SetActive(pool_values.defaultValues.IsEnable);
                }
                else
                {
                    reusedGameObject = Object.Instantiate(original, position, rotation, parent);
                }
            }
            else
            {
                pool_values = (new Stack<Object>(), new DefaultGameObjectValues(original));
                _pools.Add(original, pool_values);
                reusedGameObject = Object.Instantiate(original, position, rotation, parent);
            }

            _usedObjects.Add(reusedGameObject, (reusedGameObject, pool_values.pool));

            return reusedGameObject;
        }

        public GameObject Instantiate(GameObject original, Transform parent = null,
            bool instantiateInWorldSpace = false)
        {
            GameObject reusedGameObject;

            if (_pools.TryGetValue(original, 
                out (Stack<Object> pool, DefaultGameObjectValues defaultValues) pool_values))
            {
                Object reusedObject = null;
                while (pool_values.pool.TryPop(out reusedObject) && reusedObject == null) { };

                if(reusedObject != null)
                {
                    reusedGameObject = reusedObject as GameObject;
                    SetUpTransform(reusedGameObject.transform, parent, pool_values.defaultValues,
                        instantiateInWorldSpace);
                    reusedGameObject.SetActive(pool_values.defaultValues.IsEnable);
                }
                else
                {
                    reusedGameObject = Object.Instantiate(original, parent, instantiateInWorldSpace);
                }
            }
            else
            {
                pool_values = (new Stack<Object>(), new DefaultGameObjectValues(original));
                _pools.Add(original, pool_values);
                reusedGameObject = Object.Instantiate(original, parent, instantiateInWorldSpace);
            }

            _usedObjects.Add(reusedGameObject, (reusedGameObject, pool_values.pool));

            return reusedGameObject;
        }

        public new T Instantiate<T>(T original, Vector3 position, Quaternion rotation, 
            Transform parent = null) where T : Component
        {
            T reusedComponent;

            if(_pools.TryGetValue(original,
                out (Stack<Object> pool, DefaultGameObjectValues defaultValues) pool_values))
            {
                Object reusedObject = null;
                while (pool_values.pool.TryPop(out reusedObject) && reusedObject == null) { };

                if(reusedObject != null)
                {
                    reusedComponent = reusedObject as T;
                    reusedComponent.transform.parent = parent;
                    reusedComponent.transform.position = position;
                    reusedComponent.transform.rotation = rotation;
                    reusedComponent.transform.localScale = pool_values.defaultValues.Scale;
                    reusedComponent.gameObject.SetActive(pool_values.defaultValues.IsEnable);
                }
                else
                {
                    reusedComponent = Object.Instantiate(original, position, rotation, parent);
                }
            }
            else
            {
                pool_values = (new Stack<Object>(), new DefaultGameObjectValues(original.gameObject));
                _pools.Add(original, pool_values);
                reusedComponent = Object.Instantiate(original, position, rotation, parent);
            }

            _usedObjects.Add(reusedComponent.gameObject, (reusedComponent, pool_values.pool));

            return reusedComponent;
        }

        public new T Instantiate<T>(T original, Transform parent = null, 
            bool worldPositionStays = false) where T : Component
        {
            T reusedComponent;

            if (_pools.TryGetValue(original, 
                out (Stack<Object> pool, DefaultGameObjectValues defaultValues) pool_values))
            {
                Object reusedObject = null;
                while (pool_values.pool.TryPop(out reusedObject) && reusedObject == null) { };

                if (reusedObject != null)
                {
                    reusedComponent = reusedObject as T;
                    SetUpTransform(reusedComponent.transform, parent, pool_values.defaultValues,
                        worldPositionStays);
                    reusedComponent.gameObject.SetActive(pool_values.defaultValues.IsEnable);
                }
                else
                {
                    reusedComponent = Object.Instantiate(original, parent, worldPositionStays);
                }
            }
            else
            {
                pool_values = (new Stack<Object>(), new DefaultGameObjectValues(original.gameObject));
                _pools.Add(original, pool_values);
                reusedComponent = Object.Instantiate(original, parent, worldPositionStays);
            }

            _usedObjects.Add(reusedComponent.gameObject, (reusedComponent, pool_values.pool));

            return reusedComponent;
        }

        public void Destroy(GameObject obj, float t)
        {
            StartCoroutine(DestroyCoroutine(obj, t));
        }

        private IEnumerator DestroyCoroutine(GameObject obj, float t)
        {
            yield return new WaitForSeconds(t);
            Destroy(obj);
        }

        public void Destroy(GameObject obj)
        {
            if (_usedObjects.TryGetValue(obj, out (Object objct, Stack<Object> pool) obj_pool))//&& objct!=null
            {
                obj.SetActive(false);
                obj.transform.parent = transform;
                obj_pool.pool.Push(obj_pool.objct);
                _usedObjects.Remove(obj);
            }
            else
            {
                Object.Destroy(obj);
            }
        }

        private void SetUpTransform(Transform target, Transform parent, 
            DefaultGameObjectValues values, bool worldPositionStays)
        {
            if(worldPositionStays)
            {
                //this is an ugly hack to make Unity set transform.worldScale
                target.parent = null;
                target.transform.localPosition = values.Position;
                target.transform.localRotation = values.Rotation;
                target.transform.localScale = values.Scale;
                target.parent = parent;
            }
            else
            {
                target.parent = parent;
                target.transform.localPosition = values.Position;
                target.transform.localRotation = values.Rotation;
                target.transform.localScale = values.Scale;
            }
        }
    }
}
