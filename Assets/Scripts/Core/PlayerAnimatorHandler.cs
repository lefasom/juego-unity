using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    public Animator Animador;
    private PlayerMotor motor;

    void Awake()
    {
        if (!Animador) Animador = GetComponent<Animator>();
        motor = GetComponent<PlayerMotor>();
    }

    public void ActualizarLocomocion(float magnitud, bool corriendo, bool enPiso, bool agachado)
    {
        Animador.SetFloat("estaCaminando", magnitud);
        Animador.SetBool("estaCorriendo", corriendo);
        Animador.SetBool("estaEnElPiso", enPiso);
        Animador.SetBool("estaAgachado", agachado);
    }

    // --- EVENTOS DE ANIMACIÓN ---
    // Estos métodos son llamados desde los Animation Events en el Inspector de Unity
    public void EjecutarRetrocesoBajo() => motor?.AplicarFuerzaRetroceso(false);
    public void EjecutarRetrocesoAlto() => motor?.AplicarFuerzaRetroceso(true);
    public void EventoDeSaltoFisico() => motor?.AplicarImpulsoSalto();
    public void EventoCaidaBrusca(float fuerza) => motor?.AplicarCaidaEnPicada(fuerza);

    // --- DISPARO DE IMPACTOS (DIRECTO) ---
    public void DispararImpactoBajo()
    {
        Animador.ResetTrigger("RecibirImpactoAlto");
        Animador.SetTrigger("RecibirImpactoBajo");
    }

    public void DispararImpactoAlto()
    {
        Animador.ResetTrigger("RecibirImpactoBajo");
        Animador.SetTrigger("RecibirImpactoAlto");
    }

    // --- ATAQUES ---
    public void DispararGolpe(bool agachado)
    {
        // Limpiar triggers de patada para evitar que se disparen por error después
        Animador.ResetTrigger("PatearAgachado");
        Animador.ResetTrigger("Patear");

        if (agachado) 
        {
            Animador.SetTrigger("GolpearAgachado");
        }
        else 
        {
            Animador.SetTrigger("Golpear");
        }
    }

    public void DispararPatada(bool agachado)
    {
        // Limpiar triggers de golpe
        Animador.ResetTrigger("GolpearAgachado");
        Animador.ResetTrigger("Golpear");

        if (agachado) 
        {
            Animador.SetTrigger("PatearAgachado");
        }
        else 
        {
            Animador.SetTrigger("Patear");
        }
    }

    // --- UTILIDADES ---
    public void DispararSalto() => Animador.SetTrigger("Saltar");
}