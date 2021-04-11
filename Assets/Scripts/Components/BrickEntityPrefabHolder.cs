using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

public class BrickEntityPrefabHolder : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs{

    public static Entity brickPrefabEntity;

    public GameObject brickPrefabGameObject;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {

        Entity prefabEntity = conversionSystem.GetPrimaryEntity(brickPrefabGameObject);
        BrickEntityPrefabHolder.brickPrefabEntity = prefabEntity;
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(brickPrefabGameObject);
    }
}
