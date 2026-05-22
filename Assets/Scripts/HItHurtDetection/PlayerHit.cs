using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // ==================================================
    // REFERENCIAS
    // ==================================================

    private PlayerCombat combat;

    // ==================================================
    // CONFIGURACIÓN HITBOX
    // ==================================================

    [Header("Detección")]

    public float radioDeteccion = 1.2f;

    public float distanciaMinimaGolpe = 0.8f;

    public Vector3 offset = Vector3.zero;

    // ==================================================
    // ESTADO HITBOX
    // ==================================================

    private bool activa = false;

    // ==================================================
    // PROTECCIÓN MULTI-HIT
    // ==================================================

    /*
        Guarda objetivos ya golpeados
        durante ESTE ataque.
    */

    private HashSet<GameObject> objetivosGolpeados =
        new HashSet<GameObject>();

    // ==================================================
    // START
    // ==================================================

    void Start()
    {
        combat = GetComponentInParent<PlayerCombat>();

        if (combat == null)
        {
            Debug.LogError(
                "[PLAYER HIT] ERROR -> No se encontró PlayerCombat."
            );
        }
    }

    // ==================================================
    // UPDATE
    // ==================================================

    void Update()
    {
        if (!activa)
            return;

        DetectarGolpe();
    }

    // ==================================================
    // DETECCIÓN DE GOLPE
    // ==================================================

    void DetectarGolpe()
    {
        Vector3 centro =
            transform.position +
            transform.TransformDirection(offset);

        Collider[] hits =
            Physics.OverlapSphere(
                centro,
                radioDeteccion
            );

        foreach (Collider hit in hits)
        {
            // --------------------------------------------------
            // BUSCAR PLAYERHURT
            // --------------------------------------------------

            PlayerHurt playerHurt =
                hit.GetComponentInParent<PlayerHurt>();

            if (playerHurt == null)
                continue;

            // --------------------------------------------------
            // EVITAR AUTO GOLPE
            // --------------------------------------------------

            if (playerHurt.transform.root == transform.root)
                continue;

            // --------------------------------------------------
            // VALIDAR DISTANCIA
            // --------------------------------------------------

            float distancia =
                Vector3.Distance(
                    centro,
                    hit.ClosestPoint(centro)
                );

            if (distancia > distanciaMinimaGolpe)
                continue;

            // --------------------------------------------------
            // OBJETIVO
            // --------------------------------------------------

            GameObject objetivo =
                playerHurt.transform.root.gameObject;

            // --------------------------------------------------
            // PROTECCIÓN MULTI-HIT
            // --------------------------------------------------

            if (objetivosGolpeados.Contains(objetivo))
            {
                Debug.Log(
                    "[PLAYER HIT] MULTI-HIT BLOQUEADO -> "
                    + combat.gameObject.name
                    + " intentó volver a golpear a "
                    + objetivo.name
                );

                continue;
            }

            // --------------------------------------------------
            // REGISTRAR OBJETIVO
            // --------------------------------------------------

            objetivosGolpeados.Add(objetivo);

            // --------------------------------------------------
            // INFORMACIÓN ATAQUE
            // --------------------------------------------------

            string atacante =
                combat.gameObject.name;

            string receptor =
                objetivo.name;

            string tipoGolpe =
                combat.TipoGolpeActual;

            string reaccion =
                "RecibirImpactoAlto";

            // --------------------------------------------------
            // DEBUG PRINCIPAL
            // --------------------------------------------------

            Debug.Log(
                "\n" +
                "==============================\n" +
                "      IMPACTO DETECTADO\n" +
                "==============================\n" +
                "ATACANTE : " + atacante + "\n" +
                "OBJETIVO : " + receptor + "\n" +
                "GOLPE    : " + tipoGolpe + "\n" +
                "REACCIÓN : " + reaccion + "\n" +
                "=============================="
            );

            // --------------------------------------------------
            // APLICAR DAÑO
            // --------------------------------------------------

            playerHurt.RecibirGolpe(
                combat.TipoGolpeActual
            );

            // --------------------------------------------------
            // SINGLE TARGET
            // --------------------------------------------------

            /*
                Si querés estilo Mortal Kombat
                donde solo golpea un enemigo,
                descomentá esto.
            */

            /*
            Desactivar();
            return;
            */
        }
    }

    // ==================================================
    // ACTIVAR HITBOX
    // ==================================================

    public void Activar()
    {
        activa = true;

        // --------------------------------------------------
        // REINICIAR LISTA DE GOLPEADOS
        // --------------------------------------------------

        objetivosGolpeados.Clear();

        Debug.Log(
            "\n" +
            "==============================\n" +
            " HITBOX ACTIVADA\n" +
            "==============================\n" +
            "ATACANTE : " + combat.gameObject.name + "\n" +
            "TIPO     : " + combat.TipoGolpeActual + "\n" +
            "=============================="
        );
    }

    // ==================================================
    // DESACTIVAR HITBOX
    // ==================================================

    public void Desactivar()
    {
        activa = false;

        Debug.Log(
            "\n" +
            "==============================\n" +
            " HITBOX DESACTIVADA\n" +
            "==============================\n" +
            "ATACANTE : " + combat.gameObject.name + "\n" +
            "=============================="
        );
    }

    // ==================================================
    // DEBUG VISUAL
    // ==================================================

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 centro =
            transform.position +
            transform.TransformDirection(offset);

        Gizmos.DrawWireSphere(
            centro,
            radioDeteccion
        );
    }
}