using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 360.0f;
    public float suavizadoAnimacion = 10.0f;
    public float fuerzaSalto = 8.0f; // Fuerza del salto

    private Animator animator;
    private Rigidbody rb;
    private bool estaEnSuelo = true;

    private float inputXActual = 0f;
    private float inputYActual = 0f;
    private Vector3 direccionMovimientoActual = Vector3.zero;
    private float inputXRaw = 0f;
    private float inputYRaw = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Capturar entrada del teclado
        Vector2 entradaTeclado = Vector2.zero;
        var keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) entradaTeclado.y = 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) entradaTeclado.y = -1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) entradaTeclado.x = 1f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) entradaTeclado.x = -1f;

            // ✅ SALTO con tecla X
            if (keyboard.xKey.wasPressedThisFrame && estaEnSuelo)
            {
                Saltar();
            }
        }

        // Calcular dirección del movimiento
        Vector3 direccionMovimiento = new Vector3(entradaTeclado.x, 0.0f, entradaTeclado.y);
        if (direccionMovimiento.sqrMagnitude > 1f) direccionMovimiento.Normalize();

        inputXRaw = entradaTeclado.x;
        inputYRaw = entradaTeclado.y;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        direccionMovimientoActual = forward * inputYRaw + right * inputXRaw;
        if (direccionMovimientoActual.sqrMagnitude > 1f) direccionMovimientoActual.Normalize();

        // Parámetros al Blend Tree
        if (animator != null)
        {
            float dt = Time.deltaTime;
            inputXActual = Mathf.Lerp(inputXActual, entradaTeclado.x, dt * suavizadoAnimacion);
            inputYActual = Mathf.Lerp(inputYActual, entradaTeclado.y, dt * suavizadoAnimacion);

            animator.SetFloat("xVal", inputXActual);
            animator.SetFloat("yVal", inputYActual);
            animator.SetBool("estaEnSuelo", estaEnSuelo);
        }
    }

    void Saltar()
    {
        estaEnSuelo = false;
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);

        if (animator != null)
        {
            animator.SetTrigger("Saltar");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detectar si está tocando el suelo
        if (collision.gameObject.CompareTag("Terreno"))
        {
            estaEnSuelo = true;
        }
    }

    void FixedUpdate()
    {
        if (direccionMovimientoActual.sqrMagnitude > 0.000001f)
        {
            float fixedDt = Time.fixedDeltaTime;
            bool hasRb = rb != null;
            float absInputX = Mathf.Abs(inputXRaw);

            Vector3 desplazamiento = direccionMovimientoActual * velocidadMovimiento * fixedDt;

            if (hasRb)
            {
                rb.MovePosition(rb.position + desplazamiento);

                if (inputYRaw >= 0f)
                {
                    Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimientoActual);
                    rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, rotacionObjetivo, velocidadRotacion * fixedDt));
                }
                else
                {
                    if (absInputX > 0.01f)
                    {
                        float giroGrados = inputXRaw * velocidadRotacion * fixedDt;
                        Quaternion giro = Quaternion.Euler(0f, giroGrados, 0f);
                        rb.MoveRotation(rb.rotation * giro);
                    }
                }
            }
            else
            {
                transform.Translate(desplazamiento, Space.World);

                if (inputYRaw >= 0f)
                {
                    Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimientoActual);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, velocidadRotacion * fixedDt);
                }
                else
                {
                    if (absInputX > 0.01f)
                    {
                        float giroGrados = inputXRaw * velocidadRotacion * fixedDt;
                        transform.Rotate(0f, giroGrados, 0f, Space.Self);
                    }
                }
            }
        }
    }
}