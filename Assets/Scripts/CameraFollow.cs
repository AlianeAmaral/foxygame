using UnityEngine;

/// <summary>
/// Faz a câmera seguir um alvo (geralmente o jogador) com suavização
/// e limites (clamps) para não sair da área da fase.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    // Referência ao Transform do jogador (arraste-o no Inspector)
    public Transform target;

    // Quanto a câmera "segue" o alvo a cada frame. Valores baixos = mais suave/lento (0.01 - 0.2 típico).
    public float smoothSpeed = 0.125f;

    // Offset entre a posição do alvo e a posição desejada da câmera
    // (ex: (0, 2, -10) para a câmera ficar acima do jogador e atrás na Z).
    public Vector3 offset;

    // Limites do mapa—evitam que a câmera ultrapasse as bordas da fase.
    // Ex.: limitLeft = -10, limitRight = 50, limitDown = -5, limitUp = 20
    public float limitRight, limitLeft, limitUp, limitDown;

    // FixedUpdate é chamado em intervalos fixos (físicos). Em muitas situações
    // para câmera é melhor LateUpdate() para reduzir "jitter" quando o jogador se move em Update.
    void FixedUpdate()
    {
        if (target != null) // evita NullReferenceException se o target não estiver setado
        {
            // posição desejada = posição do jogador + offset
            Vector3 desiredPosition = target.position + offset;

            // mantemos o Z da câmera atual (importante em 2D: câmera tem Z fixo, geralmente -10)
            desiredPosition.z = transform.position.z;

            // interpola suavemente entre posição atual e a desejada.
            // Lerp com um parâmetro pequeno gera uma suavização tipo "seguindo com atraso".
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // aplica a posição suavizada
            transform.position = smoothedPosition;

            // então aplicamos os limites: se smoothedPosition estiver fora do range,
            // Mathf.Clamp garante que a posição final fique dentro dos limites.
            transform.position = new Vector3(
                Mathf.Clamp(smoothedPosition.x, limitLeft, limitRight),
                Mathf.Clamp(smoothedPosition.y, limitDown, limitUp),
                smoothedPosition.z
            );
        }
    }
}
