// Copyright (c) 2024 Pavel Teslenko
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using UnityEngine;

namespace GameObjectPooling
{
    public struct DefaultGameObjectValues
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }
        public bool IsEnable { get; }

        public DefaultGameObjectValues(GameObject prefab)
        {
            Position = prefab.transform.position;
            Rotation = prefab.transform.rotation;
            Scale = prefab.transform.localScale;
            IsEnable = prefab.activeSelf;
        }

    }
}
