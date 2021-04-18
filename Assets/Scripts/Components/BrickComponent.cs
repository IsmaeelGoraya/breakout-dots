using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using SpriteRenderer = Unity.U2D.Entities.SpriteRenderer;

public class BrickComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float4 defaultColor = new float4(0.5f, 0.5f, 0.5f, 1);

        SpriteRenderer spriteRenderer = dstManager.GetComponentData<SpriteRenderer>(entity);
        spriteRenderer.Color = defaultColor;
        dstManager.SetComponentData(entity, spriteRenderer);
    }
}
