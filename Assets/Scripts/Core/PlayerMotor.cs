using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    private CharacterController controlador;
    
    [Header("Ajustes de Movimiento")]
    public float velocidadMovimiento = 5f;
    public float velocidadCorrer = 8f;
    public float gravedad = 25f;
    public float fuerzaSalto = 12f;

    [Header("Ajustes de Retroceso")]
    public float fuerzaRetrocesoAlto = 5f;
    public float fuerzaRetrocesoBajo = 3f;

    [Header("Ajustes 2.5D")]
    // Fuerza con la que el personaje es succionado de vuelta a Z = 0
    public float fuerzaSuccionZ = 15f; 

    private Vector3 velocidadVertical;
    private Vector3 impulsoDiagonal; 
    private Vector3 impulsoRetroceso;

    public Vector3 DireccionMovimiento { get; private set; }
    public bool EstaCorriendo { get; private set; }
    public bool EstaAgachado { get; private set; }

    void Awake() => controlador = GetComponent<CharacterController>();

    void Update()
    {
        AplicarGravedad();

        if (impulsoRetroceso.magnitude > 0.1f)
            impulsoRetroceso = Vector3.Lerp(impulsoRetroceso, Vector3.zero, Time.deltaTime * 5f);
        else
            impulsoRetroceso = Vector3.zero;
    }

    public void ProcesarMovimiento(float h, bool correr, bool agacharse)
    {
        EstaAgachado = agacharse && controlador.isGrounded;
        Debug.Log($"Agachado: {EstaAgachado}");
        if (EstaAgachado)
        {
            EstaCorriendo = false; 
            DireccionMovimiento = Vector3.zero;
            Mover(Vector3.zero);
            return; 
        }

        Vector3 move = new Vector3(h, 0, 0);
        EstaCorriendo = correr && move.magnitude > 0.1f;

        if (move.magnitude >= 0.1f)
        {
            float anguloY = (h > 0) ? 90f : -90f;
            transform.rotation = Quaternion.Euler(0, anguloY, 0);
            DireccionMovimiento = move.normalized;
        }
        else { DireccionMovimiento = Vector3.zero; }

        float vel = EstaCorriendo ? velocidadCorrer : velocidadMovimiento;
        Mover((DireccionMovimiento * vel) + impulsoDiagonal + impulsoRetroceso);
    }

    public void Mover(Vector3 vectorHorizontal)
    {
        // Calculamos cuánto se ha desviado el personaje del eje Z
        float desvioZ = transform.position.z;
        
        // Creamos una fuerza correctiva negativa al desvío (si está en Z=1, la fuerza es -1)
        float correccionZ = -desvioZ * fuerzaSuccionZ;

        Vector3 movimientoFinal = vectorHorizontal + velocidadVertical;
        
        // Aplicamos la corrección directamente en el vector de movimiento
        movimientoFinal.z = correccionZ; 
        
        controlador.Move(movimientoFinal * Time.deltaTime);

        if (controlador.isGrounded) impulsoDiagonal = Vector3.zero;
    }

    public void AplicarFuerzaRetroceso(bool esAlto)
    {
        float fuerza = esAlto ? fuerzaRetrocesoAlto : fuerzaRetrocesoBajo;
        impulsoRetroceso = -transform.forward * fuerza;
    }

    public void AplicarImpulsoSalto() => velocidadVertical.y = fuerzaSalto;

public void AplicarCaidaEnPicada(float fuerza)
{
    // Forzamos la velocidad vertical a ser negativa inmediatamente
    velocidadVertical.y = -fuerza; 
    
    // Calculamos la dirección hacia adelante en el eje X (2.5D)
    float impulsoX = transform.forward.x * (fuerza * 0.5f); 
    impulsoDiagonal = new Vector3(impulsoX, 0, 0);
    
    // Opcional: Ejecutar un movimiento pequeño inmediato para romper la inercia
    controlador.Move(velocidadVertical * Time.deltaTime);
}
    private void AplicarGravedad()
    {
        if (controlador.isGrounded && velocidadVertical.y < 0) velocidadVertical.y = -2f;
        else velocidadVertical.y -= gravedad * Time.deltaTime;
    }

    public bool EstaEnPiso() => controlador.isGrounded;
}