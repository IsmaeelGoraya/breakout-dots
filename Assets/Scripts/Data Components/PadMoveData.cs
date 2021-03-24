using Unity.Entities;

[GenerateAuthoringComponent]
public struct PadMovementData : IComponentData
{
    public int direction;
    public float speed;
}