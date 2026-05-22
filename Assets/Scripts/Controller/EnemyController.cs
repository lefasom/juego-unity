using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ==================================================
    // REFERENCIAS
    // ==================================================

    private PlayerMotor motor;
    private PlayerCombat combat;
    private PlayerAnimatorHandler animatorHandler;
    private EnemyAi ai;

    // ==================================================
    // CONFIGURACIÓN
    // ==================================================

    [Header("Configuración")]

    /*
        Define hacia qué lado
        inicia mirando el enemigo.
    */

    public bool esJugadorUno = false;

    // ==================================================
    // START
    // ==================================================

    void Start()
    {
        // --------------------------------------------------
        // BUSCAR COMPONENTES
        // --------------------------------------------------

        motor = GetComponent<PlayerMotor>();

        combat = GetComponent<PlayerCombat>();

        animatorHandler =
            GetComponent<PlayerAnimatorHandler>();

        ai = GetComponent<EnemyAi>();

        // --------------------------------------------------
        // VALIDACIÓN
        // --------------------------------------------------

        if (
            motor == null ||
            combat == null ||
            animatorHandler == null ||
            animatorHandler.Animador == null ||
            ai == null
        )
        {
            Debug.LogError(
                "[ENEMY CONTROLLER] Faltan componentes.",
                this
            );

            enabled = false;

            return;
        }

        // --------------------------------------------------
        // ROTACIÓN INICIAL
        // --------------------------------------------------

        float rotY =
            esJugadorUno
            ? 90f
            : -90f;

        transform.rotation =
            Quaternion.Euler(0, rotY, 0);
    }

    // ==================================================
    // UPDATE
    // ==================================================

    void Update()
    {
        // --------------------------------------------------
        // ESTADOS LÓGICOS
        // --------------------------------------------------

        bool recibiendoDanio =
            combat.EstaRecibiendoDanio;

        bool golpeando =
            combat.EstaGolpeando;

        bool enPiso =
            motor.EstaEnPiso();

        // --------------------------------------------------
        // BLOQUEOS GENERALES
        // --------------------------------------------------

        /*
            El enemigo NO puede:
            - atacar mientras golpea
            - actuar mientras recibe daño
            - actuar en el aire
        */

        bool bloqueado =
            recibiendoDanio ||
            golpeando ||
            !enPiso;

        // --------------------------------------------------
        // SI ESTÁ BLOQUEADO
        // --------------------------------------------------

        if (bloqueado)
        {
            /*
                Frenamos completamente
                el movimiento.
            */

            motor.ProcesarMovimiento(
                0,
                false,
                false
            );

            Debug.Log(
                "[ENEMY CONTROLLER] BLOQUEADO | " +
                "Daño: " + recibiendoDanio +
                " | Golpeando: " + golpeando +
                " | En Piso: " + enPiso
            );

            return;
        }

        // --------------------------------------------------
        // MOVIMIENTO NORMAL IA
        // --------------------------------------------------

        motor.ProcesarMovimiento(
            ai.DireccionMovimiento,
            ai.Corriendo,
            false
        );

        // --------------------------------------------------
        // ACTUALIZAR ANIMACIONES
        // --------------------------------------------------

        animatorHandler.ActualizarLocomocion(
            motor.DireccionMovimiento.magnitude,
            motor.EstaCorriendo,
            motor.EstaEnPiso(),
            false
        );
    }
}