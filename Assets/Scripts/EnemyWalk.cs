using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Faz um inimigo andar automaticamente entre dois limites no eixo X.
/// Quando chega em um limite, ele inverte a direção e flipa o sprite.
/// </summary>
public class EnemyWalk : MonoBehaviour
{
    // Posição mínima no eixo X que o inimigo pode andar
    public float leftX = -2f;

    // Posição máxima no eixo X que o inimigo pode andar
    public float rightX = 2f;

    // Velocidade do movimento (unidades por segundo)
    public float speed = 2f;

    // Define se o inimigo está indo para a direita
    private bool goingRight = true;

    // Referência ao SpriteRenderer, usada para virar o sprite
    private SpriteRenderer render;

    void Start()
    {
        // Obtém o componente SpriteRenderer do inimigo (usado para flipar o sprite)
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // A cada frame, atualiza o movimento do inimigo
        WalkMovement();
    }

    // Função responsável pelo movimento horizontal
    void WalkMovement()
    {
        // Se o inimigo está indo para a direita
        if (goingRight)
        {
            // Move o inimigo no eixo X positivo (direita)
            transform.position += Vector3.right * speed * Time.deltaTime;

            // Quando atinge o limite direito, troca de direção
            if (transform.position.x >= rightX)
            {
                goingRight = false;
                render.flipX = false; // Vira o sprite para a esquerda
            }
        }
        // Caso contrário, está indo para a esquerda
        else
        {
            // Move o inimigo no eixo X negativo (esquerda)
            transform.position += Vector3.left * speed * Time.deltaTime;

            // Quando atinge o limite esquerdo, troca de direção
            if (transform.position.x <= leftX)
            {
                goingRight = true;
                render.flipX = true; // Vira o sprite para a direita
            }
        }
    }
}