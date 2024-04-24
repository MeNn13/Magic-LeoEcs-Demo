﻿using UnityEngine;

namespace Assets.Code.ECS.Status.Pool
{
    internal class PyroParticlePool : ParticlePool
    {
        public PyroParticlePool(ParticleSystem prefab, int prewarmObjectCount, string poolName) 
            : base(prefab, prewarmObjectCount, poolName)
        {

        }
    }
}