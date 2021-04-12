using System.Collections.Generic;
using DataModels;
using Factories;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickSpawnerSystem : JobComponentSystem
{
    private List<Entity> createdBricks;
    private float2 startSpawnPosition;
    private float2 nextSpwanPosition;
    private float brickSpacing;
    private Level currentLevel;
    private float2 brickSize;
    private List<Brick> currentLevelTotalBricks;

    protected override void OnCreate(){

        createdBricks = new List<Entity>();
        brickSpacing = 0.1f;
        brickSize = new float2(1.0f, 0.2f);
        currentLevel = new Level(1, Random.Range(3, 5), Random.Range(1, 4) * 3);
    }

    protected override void OnStartRunning(){

        CreateNewLevel(currentLevel);
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps){

        return default;
    }

    private void CreateBrickEntities(){

        for (int i = 0; i < currentLevelTotalBricks.Count; i++)
        {
            Entity spawnedEntity = EntityManager.Instantiate(BrickEntityPrefabHolder.brickPrefabEntity);
            createdBricks.Add(spawnedEntity);
        }
    }

    private void PositionEntites(){

        CalculateStartBrickSpawnPosition();

        for (int i = 0; i < currentLevel.Rows; i++)
        {
            for (int j = 0; j < currentLevel.Columns; j++)
            {
                Entity brickEntity = createdBricks[i + (j + ((currentLevel.Columns - 1) * i))];
                //BrickData brickEntityData = EntityManager.GetComponentData<BrickData>(brickEntity);
                //SpriteRenderer brickSpriteRenderer = EntityManager.GetComponentData<SpriteRenderer>(brickEntity);

                nextSpwanPosition = startSpawnPosition + new float2(j * (brickSize.x + brickSpacing), -i * (brickSize.y + brickSpacing));

                EntityManager.SetComponentData(brickEntity, new Translation
                {
                    Value = new float3(nextSpwanPosition.x, nextSpwanPosition.y, 0)
                });

                //brickEntityData.brickModel = currentLevelTotalBricks[i + (j + ((currentLevel.Columns - 1) * i))];

                //brickSpriteRenderer.Color = new float4(brickEntityData.brickModel.Color.r, brickEntityData.brickModel.Color.g, brickEntityData.brickModel.Color.b, 1.0f);

                //EntityManager.SetComponentData(brickEntity, brickSpriteRenderer);
            }
        }
    }

    private void CreateNewLevel(Level level){

        //Create brick models
        currentLevelTotalBricks = BrickFactory.CreateBricks(level);
        //Create brick objects
        CreateBrickEntities();
        //Position bricks in row and column
        PositionEntites();
    }

    private void CalculateStartBrickSpawnPosition(){

        Vector2 screenDimension = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height));
        startSpawnPosition = new float2(screenDimension.x, screenDimension.y);
        startSpawnPosition.y -= 1.0f;
        startSpawnPosition.x += ((brickSize.x * currentLevel.Columns) / 2) * -1;
        startSpawnPosition.x +=  brickSpacing * currentLevel.Columns;
    }
}
