# Proyecto de Combate en Unity - Flujo y Tareas

## 1. Flujo del Sistema de Combate

### Preguntas Clave y Respuestas

- **¿Qué pasa cuando presiono un botón para atacar?**
  Cuando se presiona un botón, el controlador decide el tipo de ataque (puño, patada, agachado) y activa la animación correspondiente.
- **¿Cómo se detecta un golpe?**
  Se usan colisionadores (esferas) en las manos o pies. Si la esfera toca la zona vulnerable del rival, se detecta el golpe.
- **¿Qué ocurre al detectar un golpe?**
  Se le dice al rival que reciba daño y se activa una animación de reacción.
- **¿Por qué no se permite recibir daño varias veces seguidas?**
  Porque se activa un estado lógico que impide nuevos golpes mientras dura la animación de daño.
- **¿Quién controla que no se interrumpa la reacción?**
  El script de PlayerCombat usa una bandera (un estado) para saber si está recibiendo daño.
- **¿Cómo se reinicia después del golpe?**
  Cuando termina la animación, se llama a una función que indica que ya no se recibe más daño.
- **¿Qué hace el Animator?**
  Reproduce las animaciones (golpes, impactos, caídas) mediante triggers que se activan desde el código.

## 2. Análisis Técnico y Mejoras

- Implementar la protección multi-hit usando una lista de objetivos golpeados, para evitar daño repetido.
- Unificar el comportamiento de Player y CPU, asegurando que ambos respeten los mismos estados (no golpear cayendo, no moverse en hitstun).
- Refinar las hitboxes y hurtboxes ajustando su tamaño y precisión.

## 3. Tareas por Hacer

- [X]  Protección multi-hit: Implementar la lista de enemigos golpeados en PlayerHit.
- [ ]  Verificar que CPU respete estados de daño igual que el Player.
- [ ]  Refinar hitboxes/hurtboxes para evitar detección accidental.
- [X]  Bloqueo de movimiento durante daño: ya implementado.
- [X]  Animación de reacción al golpe: ya configurada en Animator.

## 4. Estado del Proyecto

Actualmente, el flujo principal de ataques, detección de golpes,
