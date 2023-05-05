using UnityEngine;

public class Game : MonoBehaviour
{
    void Start()
    {
        new SnakeControl(new SnakeModel(), new SnakeView());
    }
    void Update()
    {
        
    }
}