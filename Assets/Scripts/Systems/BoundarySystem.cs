using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public class BoundarySystem : JobComponentSystem
{

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        Entities
            .WithoutBurst()
            .ForEach((ref Translation topBoundaryTrans, ref PhysicsCollider collider, in TopBoundaryTag topBoundaryTag) =>
            {
                // make sure we are dealing with spheres
                if (collider.Value.Value.Type != ColliderType.Box) return;

                unsafe
                {
                    // grab the sphere pointer
                    BoxCollider* scPtr = (BoxCollider*)collider.ColliderPtr;

                    // update the collider geometry
                    var BoxGeometry = scPtr->Geometry;
                    BoxGeometry.Size = new float3(GameManager.Instance.ScreenSizeInWorldSpace.x, 0.1f, 0.1f); ;
                    scPtr->Geometry = BoxGeometry;
                }

                topBoundaryTrans.Value = new float3(0, GameManager.Instance.bounds.y, 0);
            }).Run();

        Entities
           .WithoutBurst()
           .ForEach((ref Translation bottomBoundaryTrans, ref PhysicsCollider collider, in BottomBoundaryTag bottomBoundaryTag) =>
           {
               // make sure we are dealing with spheres
               if (collider.Value.Value.Type != ColliderType.Box) return;


               unsafe
               {
                   // grab the sphere pointer
                   BoxCollider* scPtr = (BoxCollider*)collider.ColliderPtr;

                   // update the collider geometry
                   var BoxGeometry = scPtr->Geometry;
                   BoxGeometry.Size = new float3(GameManager.Instance.ScreenSizeInWorldSpace.x, 0.1f, 0.1f); ;
                   scPtr->Geometry = BoxGeometry;
               }

               bottomBoundaryTrans.Value = new float3(0, -GameManager.Instance.bounds.y, 0);
           }).Run();

        Entities
          .WithoutBurst()
          .ForEach((ref Translation leftBoundaryTrans, ref PhysicsCollider collider, in LeftBoundaryTag leftBoundaryTag) =>
          {
              // make sure we are dealing with spheres
              if (collider.Value.Value.Type != ColliderType.Box) return;

              unsafe
              {
                  // grab the sphere pointer
                  BoxCollider* scPtr = (BoxCollider*)collider.ColliderPtr;

                  // update the collider geometry
                  var BoxGeometry = scPtr->Geometry;
                  BoxGeometry.Size = new float3(0.1f, GameManager.Instance.ScreenSizeInWorldSpace.y, 0.1f); ;
                  scPtr->Geometry = BoxGeometry;
              }

              leftBoundaryTrans.Value = new float3(-GameManager.Instance.bounds.x, 0, 0);
          }).Run();

        Entities
         .WithoutBurst()
         .ForEach((ref Translation rightBoundaryTrans, ref PhysicsCollider collider, in RightBoundaryTag rightBoundaryTag) =>
         {
             // make sure we are dealing with spheres
             if (collider.Value.Value.Type != ColliderType.Box) return;

             unsafe
             {
                 // grab the sphere pointer
                 BoxCollider* scPtr = (BoxCollider*)collider.ColliderPtr;

                 // update the collider geometry
                 var BoxGeometry = scPtr->Geometry;
                 BoxGeometry.Size = new float3(0.1f, GameManager.Instance.ScreenSizeInWorldSpace.y, 0.1f); ;
                 scPtr->Geometry = BoxGeometry;
             }

             rightBoundaryTrans.Value = new float3(GameManager.Instance.bounds.x, 0, 0);
         }).Run();

    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return default;
    }
}