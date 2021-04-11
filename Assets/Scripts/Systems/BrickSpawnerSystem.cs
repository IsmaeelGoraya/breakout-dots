using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BrickSpawnerSystem : JobComponentSystem
{
    protected override void OnStartRunning()
    {
        Entity spawnedEntity = EntityManager.Instantiate(BrickEntityPrefabHolder.brickPrefabEntity);

        EntityManager.SetComponentData(spawnedEntity, new Translation
        {
            Value = new float3(-2, 0, 0)
        });
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return default;
    }
}
