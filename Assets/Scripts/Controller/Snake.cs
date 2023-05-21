using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private SnakeModel snake{get;set;}
    [SerializeField] private Player player{get;set;}

    void Awake(){
        snake = new SnakeModel();
        player = GetComponent<Player>();
        player.OnStartMoved += snake.GetSpeed;

        snake.OnMoved += movement => player.Move(movement.moveSpeed, movement.rotateMoveSpeed);
        snake.OnMoved += movement => player.AddDrag(movement.dragOnMove, movement.dragOnStop);
    }
}