using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerAnimatorHandler animatorHandler;

    // --------------------------------------------------
    // ESTADO ACTUAL DE ATAQUE
    // --------------------------------------------------

    // Esto sirve para saber si el personaje
    // está ejecutando un golpe o una patada.
    public bool EstaGolpeando { get; private set; }

    // --------------------------------------------------
    // TIPO DE GOLPE ACTUAL
    // --------------------------------------------------

    // Guarda el último tipo de ataque ejecutado.
    // Puede servir para:
    // - efectos
    // - sonidos
    // - daño diferente
    // - IA
    // - partículas
    public string TipoGolpeActual { get; private set; } = "Normal";
    public bool EstaRecibiendoDanio { get; private set; }
    // --------------------------------------------------
    // HITBOXES
    // --------------------------------------------------

    [Header("HITBOXES (arrastrar objetos con PlayerHit)")]
    public PlayerHit manoDerecha;
    public PlayerHit pieDerecho;
    void Awake()
    {
        motor = GetComponent<PlayerMotor>();
        animatorHandler = GetComponent<PlayerAnimatorHandler>();

        // --------------------------------------------------
        // BÚSQUEDA AUTOMÁTICA DE HITBOXES
        // --------------------------------------------------

        if (manoDerecha == null)
        {
            Transform t = transform.Find("ManoDerecha");

            if (t != null)
                manoDerecha = t.GetComponent<PlayerHit>();
        }

        if (pieDerecho == null)
        {
            Transform t = transform.Find("PieDerecho");

            if (t != null)
                pieDerecho = t.GetComponent<PlayerHit>();
        }
    }

    void Update()
    {
        // Seguridad por si falta Animator
        if (animatorHandler == null || animatorHandler.Animador == null)
            return;

        AnimatorStateInfo state =
            animatorHandler.Animador.GetCurrentAnimatorStateInfo(0);

        // --------------------------------------------------
        // DETECTAMOS SI ESTÁ ATACANDO
        // --------------------------------------------------

        // Esto depende de los TAGS del Animator.
        // Tus animaciones deben tener:
        //
        // "Golpe"
        // o
        // "Patada"
        //
        // para que funcione correctamente.

        EstaGolpeando =
            state.IsTag("Golpe") ||
            state.IsTag("Patada");
    }

    // ==================================================
    // GOLPE DE MANO
    // ==================================================
    public void IniciarDanio()
    {
        EstaRecibiendoDanio = true;

        // Mientras recibe daño,
        // dejamos de atacar automáticamente.
        EstaGolpeando = false;

        // Seguridad extra:
        // desactivamos hitboxes activas.
        DesactivarTodas();
    }

    // ==================================================
    // FINALIZAR DAÑO
    // ==================================================

    public void FinalizarDanio()
    {
        EstaRecibiendoDanio = false;
    }
    public void IntentarGolpe()
    {
        TipoGolpeActual = "Golpe";
        // Obtenemos si está agachado
        bool agachadoActualmente = motor.EstaAgachado;
        // Disparamos animación
        animatorHandler.DispararGolpe(agachadoActualmente);
    }

    // ==================================================
    // PATADA
    // ==================================================

    public void IntentarPatada()
    {
        TipoGolpeActual = "Patada";

        if (animatorHandler != null && motor != null)
        {
            // Obtenemos si está agachado
            bool agachadoActualmente = motor.EstaAgachado;

            // Disparamos animación
            animatorHandler.DispararPatada(agachadoActualmente);
        }
    }

    // ==================================================
    // EVENTOS DE ANIMACIÓN (HITBOXES)
    // ==================================================

    // ACTIVAR MANO

    public void ActivarManoDerecha()
    {
        DesactivarTodas();

        if (manoDerecha != null)
            manoDerecha.Activar();
        else
            Debug.LogWarning("manoDerecha no asignada");
    }

    // ACTIVAR PIE

    public void ActivarPieDerecho()
    {
        DesactivarTodas();

        if (pieDerecho != null)
            pieDerecho.Activar();
        else
            Debug.LogWarning("pieDerecho no asignado");
    }

    // DESACTIVAR TODAS LAS HITBOXES

    public void DesactivarTodas()
    {
        if (manoDerecha != null)
            manoDerecha.Desactivar();

        if (pieDerecho != null)
            pieDerecho.Desactivar();
    }
}