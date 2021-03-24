using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

[AlwaysSynchronizeSystem]
public class PadMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float xBound = GameManager.Instance.bounds.x;

        Entities.ForEach((ref Translation trans, in PadMovementData movementData) =>
        {
            trans.Value.x = math.clamp(trans.Value.x + (movementData.speed * movementData.direction * deltaTime), xBound, -xBound);
        }).Run();

        return default;
    }
}
