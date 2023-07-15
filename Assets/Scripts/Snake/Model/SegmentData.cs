using System;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

using UnityEngine;

public abstract class SegmentData: ScriptableObject {
    public int mass;
    public float drag;
}