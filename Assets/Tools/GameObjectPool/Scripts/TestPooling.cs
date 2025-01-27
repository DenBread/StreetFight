// Copyright (c) 2024 Pavel Teslenko
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using UnityEngine;

namespace GameObjectPooling
{
    public class TestPooling : MonoBehaviour
    {
        [SerializeField]
        private float radius = 100;
        [SerializeField]
        private Rigidbody prefab;
        [SerializeField]
        private bool isEnablePooling = false;

        IEnumerator Start()
        {
            while (true)
            {
                Vector3 position = transform.position + GetRandomInsideUnitCircle() * radius;

                if (isEnablePooling)
                {
                    Rigidbody instance = GameObjectPool.Instantiate(prefab, position, Quaternion.identity);
                    //pool resets only transform parameters after the GameObject is reused.
                    //component parameters must be reset manually
                    instance.linearVelocity = Vector3.zero;
                }
                else
                {
                    Rigidbody instance = Instantiate(prefab, position, Quaternion.identity);
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isEnablePooling)
            {
                GameObjectPool.Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        private Vector3 GetRandomInsideUnitCircle()
        {
            Vector3 randomCorclePoint = Random.insideUnitCircle;
            return new Vector3(randomCorclePoint.x, 0f, randomCorclePoint.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}