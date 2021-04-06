using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

[AlwaysSynchronizeSystem]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public class BallSystem : JobComponentSystem
{
    protected override void OnStartRunning()
    {
        base.OnCreate();
        float2 ballRandomVelocityDirection = UnityEngine.Random.insideUnitCircle.normalized;
        Entities
            .WithoutBurst()
            .ForEach((ref Translation ballTrans, ref PhysicsVelocity ballVelocity, in BallTag ballTag) =>
        {
            ballVelocity.Angular = float3.zero;
            ballVelocity.Linear = new float3(ballRandomVelocityDirection.x, ballRandomVelocityDirection.y,0) * 7;
        }).Run();

    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        Entities
            .ForEach((ref PhysicsVelocity ballVelocity, in SpeedIncreaseOverTimeData speedIncrease) =>
        {
            float2 modifier = new float2(speedIncrease.increasePerSecond * deltaTime);

            float2 newVel = ballVelocity.Linear.xy;
            newVel += math.lerp(-modifier, modifier, math.sign(newVel));
            ballVelocity.Linear.xy = newVel;

        }).Run();

        return default;
    }
}
