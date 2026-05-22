using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerCombat combat;
    private PlayerAnimatorHandler animatorHandler;

    public bool esJugadorUno = true;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        combat = GetComponent<PlayerCombat>();
        animatorHandler = GetComponent<PlayerAnimatorHandler>();

        float rotY = esJugadorUno ? 90f : -90f;
        transform.rotation = Quaternion.Euler(0, rotY, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --------------------------------------------------
        // ESTADO LÓGICO DE DAÑO
        // --------------------------------------------------

        bool estaEnDanio = combat.EstaRecibiendoDanio;

        // --------------------------------------------------
        // BLOQUEO DE MOVIMIENTO
        // --------------------------------------------------

        if (estaEnDanio || (combat.EstaGolpeando && motor.EstaEnPiso()))
        {
            // Bloqueamos movimiento
            motor.ProcesarMovimiento(0, false, motor.EstaAgachado);
        }
        else
        {
            // Movimiento normal
            float h = Input.GetAxisRaw("Horizontal");

            motor.ProcesarMovimiento(
                h,
                Input.GetKey(KeyCode.LeftShift),
                Input.GetKey(KeyCode.S)
            );
        }

        // --------------------------------------------------
        // DEBUG IMPACTOS
        // --------------------------------------------------

        if (Input.GetKeyDown(KeyCode.C))
            animatorHandler.DispararImpactoBajo();

        if (Input.GetKeyDown(KeyCode.K))
            animatorHandler.DispararImpactoAlto();

        // --------------------------------------------------
        // SALTO
        // --------------------------------------------------

        if (
            Input.GetKeyDown(KeyCode.W)
            && motor.EstaEnPiso()
            && !motor.EstaAgachado
            && !estaEnDanio
        )
        {
            animatorHandler.DispararSalto();
        }

        // --------------------------------------------------
        // ATAQUES
        // --------------------------------------------------

        if (Input.GetMouseButtonDown(0) && !estaEnDanio)
        {
            combat.IntentarGolpe();
        }

        if (Input.GetMouseButtonDown(1) && !estaEnDanio)
        {
            combat.IntentarPatada();
        }

        // --------------------------------------------------
        // LOCOMOCIÓN
        // --------------------------------------------------

        animatorHandler.ActualizarLocomocion(
            motor.DireccionMovimiento.magnitude,
            motor.EstaCorriendo,
            motor.EstaEnPiso(),
            motor.EstaAgachado
        );
    }
}