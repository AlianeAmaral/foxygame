using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    public float topY = 3f;     // altura máxima
    public float bottomY = 1f;  // altura mínima
    public float speed = 2f;    // velocidade

    private bool goingUp = true;

    void Update()
    {
        FlyMovement();
    }

    void FlyMovement()
    {
        // se está subindo
        if (goingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y >= topY)
                goingUp = false;
        }
        else // descendo
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if (transform.position.y <= bottomY)
                goingUp = true;
        }
    }
}
