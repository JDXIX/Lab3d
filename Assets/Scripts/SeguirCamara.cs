using UnityEngine;

public class SeguirCamara : MonoBehaviour
{
    [Header("Referencias")]
    public Transform personaje; // Arrastra el personaje aquí en el Inspector

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 5, -10); // Distancia desde el personaje

    [Header("Suavizado")]
    public float suavizado = 5f; // Velocidad de seguimiento

    [Header("Opciones")]
    public bool seguirRotacion = false; // Si la cámara debe seguir la rotación del personaje
    public bool limiteVertical = false; // Si quieres limitar el movimiento vertical
    public float limiteSuperior = 10f;
    public float limiteInferior = -10f;

    void Start()
    {
        // Si no se asignó el personaje, intenta encontrarlo automáticamente
        if (personaje == null)
        {
            GameObject personajeObj = GameObject.FindGameObjectWithTag("Player");
            if (personajeObj != null)
                personaje = personajeObj.transform;
        }

        // Asegurar que el offset inicial sea correcto
        if (offset == Vector3.zero)
            offset = transform.position - personaje.position;
    }

    void LateUpdate()
    {
        if (personaje == null) return;

        Vector3 posicionDeseada;

        if (seguirRotacion)
        {
            // La cámara sigue la rotación del personaje
            posicionDeseada = personaje.position + personaje.rotation * offset;
        }
        else
        {
            // La cámara mantiene el offset fijo
            posicionDeseada = personaje.position + offset;
        }

        // Limitar movimiento vertical si está activado
        if (limiteVertical)
        {
            posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, limiteInferior, limiteSuperior);
        }

        // Movimiento suave
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
    }
}