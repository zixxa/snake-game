using System;
using UnityEngine;

public class PointSystem: MonoBehaviour
{
    private SpawnPoint spawn;
    private SnakeModel snake;
    void Start(){
        snake = new SnakeModel();
        spawn = GetComponent<SpawnPoint>();
        Segment.OnGetPoint += snake.AddSegment;
        Segment.OnGetPoint += spawn.SpawnNewPoint;
    }
}