using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

public class Head: Segment
{
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
    public string Name => name;
    public Sprite Image => image;
}