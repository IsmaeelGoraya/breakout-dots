using Unity.Entities;

[GenerateAuthoringComponent]
public struct BrickEntityPrefabHolder : IComponentData
{
    public Entity brickPrefabEntity;
}
