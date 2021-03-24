using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PlayerInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((ref PadMovementData moveData, in PadInputData inputData)=>
        {
            //Clear any old value
            moveData.direction = 0;
            //Update direction based on user input
            moveData.direction -= Input.GetKey(inputData.leftArrow) ? 1 : 0;
            moveData.direction += Input.GetKey(inputData.rightArrow) ? 1 : 0;
        }).Run();

        return default;
    }
}
