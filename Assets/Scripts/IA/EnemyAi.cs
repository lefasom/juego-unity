using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    // ==================================================
    // REFERENCIAS
    // ==================================================

    private PlayerMotor motor;
    private PlayerCombat combat;

    // ==================================================
    // OBJETIVO
    // ==================================================

    [Header("Objetivo")]

    /*
        Jugador objetivo
        que la IA perseguirá.
    */

    public Transform jugador;

    // ==================================================
    // CONFIGURACIÓN IA
    // ==================================================

    [Header("IA")]

    /*
        Distancia mínima
        para comenzar ataques.
    */

    public float distanciaAtaque = 2f;

    /*
        Tiempo entre decisiones.
    */

    public float tiempoDecision = 1f;

    // ==================================================
    // TIMER
    // ==================================================

    private float timer;

    // ==================================================
    // MOVIMIENTO ACTUAL
    // ==================================================

    /*
        Dirección horizontal:
        -1 izquierda
         1 derecha
         0 quieto
    */

    public float DireccionMovimiento
    {
        get;
        private set;
    }

    // ==================================================
    // ESTADOS IA
    // ==================================================

    public bool Corriendo
    {
        get;
        private set;
    }

    // ==================================================
    // START
    // ==================================================

    void Start()
    {
        motor = GetComponent<PlayerMotor>();

        combat = GetComponent<PlayerCombat>();
    }

    // ==================================================
    // UPDATE
    // ==================================================

    void Update()
    {
        // --------------------------------------------------
        // VALIDAR OBJETIVO
        // --------------------------------------------------

        if (jugador == null)
            return;

        // --------------------------------------------------
        // ESTADOS BLOQUEADOS
        // --------------------------------------------------

        /*
            La IA NO puede actuar:
            - recibiendo daño
            - atacando
            - en el aire
        */

        bool bloqueado =
            combat.EstaRecibiendoDanio ||
            combat.EstaGolpeando ||
            !motor.EstaEnPiso();

        // --------------------------------------------------
        // SI ESTÁ BLOQUEADO
        // --------------------------------------------------

        if (bloqueado)
        {
            DireccionMovimiento = 0f;

            Corriendo = false;

            Debug.Log(
                "[ENEMY AI] BLOQUEADO"
            );

            return;
        }

        // --------------------------------------------------
        // TIMER DECISIONES
        // --------------------------------------------------

        /*
            Evita que tome
            decisiones cada frame.
        */

        timer -= Time.deltaTime;

        if (timer > 0)
            return;

        timer = tiempoDecision;

        // --------------------------------------------------
        // TOMAR DECISIÓN
        // --------------------------------------------------

        TomarDecision();
    }

    // ==================================================
    // IA PRINCIPAL
    // ==================================================

    void TomarDecision()
    {
        // --------------------------------------------------
        // DISTANCIA AL JUGADOR
        // --------------------------------------------------

        float distancia =
            Vector3.Distance(
                transform.position,
                jugador.position
            );

        // --------------------------------------------------
        // RESETEAR ESTADOS
        // --------------------------------------------------

        Corriendo = false;

        // --------------------------------------------------
        // SI ESTÁ LEJOS
        // --------------------------------------------------

        /*
            Camina hacia el jugador.
        */

        if (distancia > distanciaAtaque)
        {
            DireccionMovimiento =
                jugador.position.x > transform.position.x
                ? 1f
                : -1f;

            Debug.Log(
                "[ENEMY AI] ACERCÁNDOSE"
            );

            return;
        }

        // --------------------------------------------------
        // SI ESTÁ CERCA
        // --------------------------------------------------

        /*
            Elige una acción aleatoria.
        */

        DireccionMovimiento = 0f;

        int accion =
            Random.Range(0, 3);

        switch (accion)
        {
            // =================================
            // GOLPE
            // =================================

            case 0:

                combat.IntentarGolpe();

                Debug.Log(
                    "[ENEMY AI] GOLPE"
                );

                break;

            // =================================
            // PATADA
            // =================================

            case 1:

                combat.IntentarPatada();

                Debug.Log(
                    "[ENEMY AI] PATADA"
                );

                break;

            // =================================
            // RETROCEDER
            // =================================

            case 2:

                DireccionMovimiento =
                    jugador.position.x > transform.position.x
                    ? -1f
                    : 1f;

                Debug.Log(
                    "[ENEMY AI] RETROCEDIENDO"
                );

                break;
        }
    }
}