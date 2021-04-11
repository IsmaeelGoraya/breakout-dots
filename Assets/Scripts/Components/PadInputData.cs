using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PadInputData : IComponentData
{
    public KeyCode leftArrow;
    public KeyCode rightArrow;
}