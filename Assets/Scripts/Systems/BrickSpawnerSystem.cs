using DataModels;
using Factories;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using SpriteRenderer = Unity.U2D.Entities.SpriteRenderer;
using Random = UnityEngine.Random;

[AlwaysSynchronizeSystem]
public class BrickSpawnerSystem : ComponentSystem
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

    protected override void OnUpdate(){
    }

    private void CreateBrickEntities(){

        BrickEntityPrefabHolder brickEntityPrefabHolder = GetSingleton<BrickEntityPrefabHolder>();

        for (int i = 0; i < currentLevelTotalBricks.Count; i++)
        {
            Entity spawnedEntity = EntityManager.Instantiate(brickEntityPrefabHolder.brickPrefabEntity);
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
                SpriteRenderer brickSpriteRenderer = EntityManager.GetComponentData<SpriteRenderer>(brickEntity);
                Translation brickTranslation = EntityManager.GetComponentData<Translation>(brickEntity);

                nextSpwanPosition = startSpawnPosition + new float2(j * (brickSize.x + brickSpacing), -i * (brickSize.y + brickSpacing));

                brickTranslation.Value = new float3(nextSpwanPosition.x, nextSpwanPosition.y, 0);
                float4 brickColor = new float4(currentLevelTotalBricks[i + (j + ((currentLevel.Columns - 1) * i))].Color.r,
                                                currentLevelTotalBricks[i + (j + ((currentLevel.Columns - 1) * i))].Color.g,
                                                currentLevelTotalBricks[i + (j + ((currentLevel.Columns - 1) * i))].Color.b,
                                                1.0f);
                brickSpriteRenderer.Color = brickColor;

                EntityManager.SetComponentData(brickEntity, brickTranslation);
                EntityManager.SetComponentData(brickEntity, brickSpriteRenderer);

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
