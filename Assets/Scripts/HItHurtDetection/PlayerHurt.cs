using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerCombat combat;

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        combat = GetComponent<PlayerCombat>();

        if (playerAnimator == null)
        {
            Debug.LogError(
                "No se encontró Animator en PlayerHurt"
            );
        }

        if (combat == null)
        {
            Debug.LogError(
                "No se encontró PlayerCombat en PlayerHurt"
            );
        }
    }

    public void RecibirGolpe(string tipoGolpe)
    {
        // --------------------------------------------------
        // EVITAMOS REINICIAR EL DAÑO
        // --------------------------------------------------
         

        if (combat.EstaRecibiendoDanio)
            return;
Debug.Log("-------------------"+tipoGolpe);
        // --------------------------------------------------
        // INICIAMOS ESTADO LÓGICO DE DAÑO
        // --------------------------------------------------

        combat.IniciarDanio();

        // --------------------------------------------------
        // REPRODUCIMOS REACCIÓN
        // --------------------------------------------------

        if (tipoGolpe == "Golpe" || tipoGolpe == "Patada")
        {
            playerAnimator.SetTrigger("RecibirImpactoAlto");
        }

    }

    // --------------------------------------------------
    // LLAMAR DESDE ANIMATION EVENT
    // --------------------------------------------------

    public void FinalizarDanioAnimacion()
    {
        combat.FinalizarDanio();
         Debug.Log("FINALIZAR DAÑO EVENT");
    }
}