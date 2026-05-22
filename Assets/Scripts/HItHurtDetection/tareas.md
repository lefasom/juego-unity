# 🎮 PROYECTO DE COMBATE 2.5D — DOCUMENTACIÓN GENERAL

---

# 📌 ESTADO ACTUAL DEL PROYECTO

## ✅ SISTEMAS IMPLEMENTADOS

- [x] Movimiento 2.5D
- [x] Sistema de salto
- [x] Sistema de gravedad
- [x] Retroceso por impacto
- [x] Caída brusca
- [x] Sistema de combate
- [x] Golpes y patadas
- [x] Hitboxes
- [x] Hurtboxes
- [x] Sistema de daño lógico
- [x] Bloqueo durante hitstun
- [x] Protección contra reinicio de daño
- [x] Eventos de animación
- [x] IA enemiga básica
- [x] Bloqueos lógicos CPU
- [x] Protección Multi-Hit
- [x] Separación lógica de responsabilidades
- [x] Flujo de combate estable
- [x] Corrección de Exit Time
- [x] Eliminación de dependencia de Tags para gameplay

---

# 🧠 ARQUITECTURA ACTUAL

---

## 🔵 PlayerController

Responsabilidad:
- Inputs
- Coordinación general
- Restricciones de gameplay

Controla:
- Movimiento
- Ataques
- Saltos
- Bloqueos

---

## 🔴 PlayerCombat

Responsabilidad:
- Estados lógicos de combate

Controla:
- EstaGolpeando
- EstaRecibiendoDanio
- TipoGolpeActual

---

## 🟠 PlayerHit

Responsabilidad:
- Detección ofensiva

Controla:
- Hitboxes
- Activación/desactivación
- Multi-hit protection

---

## 🟣 PlayerHurt

Responsabilidad:
- Recepción de golpes

Controla:
- Reacciones visuales
- Inicio de daño
- Fin de daño

---

## 🟢 PlayerMotor

Responsabilidad:
- Movimiento físico

Controla:
- Movimiento
- Saltos
- Retrocesos
- Gravedad
- Movimiento 2.5D

---

## 🟡 PlayerAnimatorHandler

Responsabilidad:
- Comunicación con Animator

Controla:
- Triggers
- Parámetros
- Animation Events

---

## ⚫ EnemyAI

Responsabilidad:
- Decisiones CPU

Controla:
- Ataques CPU
- Movimiento CPU
- Decisiones

---

## ⚪ EnemyController

Responsabilidad:
- Aplicar reglas al CPU

Controla:
- Restricciones
- Validaciones
- Bloqueos

---

# 🔄 FLUJO GENERAL DEL COMBATE

```text
INPUT
 ↓
PlayerController
 ↓
PlayerCombat
 ↓
PlayerHit
 ↓
PlayerHurt
 ↓
PlayerCombat (Daño)
 ↓
Animator
 ↓
Animation Event
 ↓
FinalizarDaño()
```

---

# ✅ SISTEMAS IMPORTANTES IMPLEMENTADOS

---

## ✅ SISTEMA DE DAÑO LÓGICO

### Antes:
❌ Animator Tags controlaban gameplay.

### Ahora:
✅ PlayerCombat controla estados reales.

---

## ✅ PROTECCIÓN MULTI-HIT

### Problema:
Un golpe detectaba múltiples veces.

### Solución:
HashSet de objetivos golpeados por ataque.

---

## ✅ CPU RESPETA REGLAS

### Ahora el CPU:
- [x] No golpea en hitstun
- [x] No golpea cayendo
- [x] No toma decisiones atacando
- [x] Respeta recuperación

---

# 📚 EXPERIENCIA GANADA

---

## ✅ UNITY

- Animator
- CharacterController
- Animation Events
- Triggers
- Estados lógicos
- Separación de responsabilidades
- Hitboxes/Hurtboxes
- Flujo de gameplay
- IA básica
- Gestión de locomoción

---

## ✅ BLENDER

- Importación
- Exportación FBX
- Escalas
- Rigging básico
- Modificación de modelos
- Exportación de animaciones

---

# 🧠 FILOSOFÍA DEL PROYECTO

---

## REGLA PRINCIPAL

### ❌ Animator NO controla gameplay.

### ✅ El código controla gameplay.

Animator:
- Solo visual.

Código:
- Lógica real.

---

# 📌 REGLAS ACTUALES DEL JUEGO

---

## EL PERSONAJE NO PUEDE:

- [x] Golpear recibiendo daño
- [x] Saltar recibiendo daño
- [x] Reiniciar caída
- [x] Moverse golpeando
- [x] Recibir daño infinito
- [x] Atacar en el aire
- [x] Auto golpearse

---

# 📌 CONFIGURACIÓN DE PERSONAJES

---

# ✅ ESTRUCTURA NECESARIA

```text
PERSONAJE
│
├── Animator
├── CharacterController
├── PlayerController / EnemyController
├── PlayerCombat
├── PlayerMotor
├── PlayerAnimatorHandler
├── PlayerHurt
│
├── Hurtbox
│   └── Layer: Hurtbox
│
└── Mano/Pie
    └── PlayerHit
        └── Layer: Hitbox
```

---

# 📌 CONFIGURACIÓN NECESARIA

## Animator

### Triggers importantes:
- Golpear
- Patear
- RecibirImpactoAlto

---

## Animation Events

### Ataques:
- Activar hitbox
- Desactivar hitbox

### Reacciones:
- Finalizar daño

---

# 🚨 TAREAS PENDIENTES

---

# 🔴 PRIORIDAD ALTA

## COMBATE

- [ ] Sistema de vida
- [ ] Sistema de daño real
- [ ] UI de vida
- [ ] Combos
- [ ] Prioridad de ataques
- [ ] Cancelaciones
- [ ] Hitstop
- [ ] Hitstun configurable

---

## IA

- [ ] Mejorar IA enemiga
- [ ] IA defensiva
- [ ] IA agresiva
- [ ] IA con persecución avanzada
- [ ] IA con cooldowns
- [ ] IA con lectura de distancia

---

## ANIMACIONES

- [ ] Mejorar calidad de animaciones
- [ ] Hacer animaciones menos rígidas
- [ ] Ajustar timings
- [ ] Mejorar transiciones

---

# 🟠 PRIORIDAD MEDIA

## GAMEPLAY

- [ ] Ajustar alcances
- [ ] Ajustar velocidades
- [ ] Ajustar retrocesos
- [ ] Ajustar gravedad
- [ ] Ajustar sensación de impacto

---

## VISUAL

- [ ] Efectos visuales
- [ ] Partículas
- [ ] Cámara de impacto
- [ ] Sonidos

---

# 🟡 PRIORIDAD BAJA

## FUTURO

- [ ] Múltiples personajes
- [ ] Sistema de selección
- [ ] Escenarios
- [ ] Menú principal
- [ ] Sistema online
- [ ] Modo versus

---

# 📌 TAREA IMPORTANTE ACTUAL

# 🔴 GITHUB / LIMPIEZA PROYECTO

- [ ] Crear `.gitignore` en carpeta raíz
- [ ] Ignorar:
  - Library/
  - Temp/
  - Logs/
  - Build/
  - Obj/
  - UserSettings/
  - Packages/
  - Animaciones pesadas
  - Modelos innecesarios
- [ ] Guardar SOLAMENTE:
  - Assets/Scripts/

---

# 📌 ESTRUCTURA GITHUB DESEADA

```text
Proyecto/
│
├── Assets/
│   └── Scripts/
│
├── README.md
├── .gitignore
└── DOCUMENTACION.md
```

---

# 🧠 LECCIONES IMPORTANTES APRENDIDAS

---

## ✅ CONTENIDO CONTROLADO

Los personajes deben tener:
- tamaños similares
- alcances similares
- velocidades coherentes

Para mantener:
- balance
- gameplay estable
- hitboxes coherentes

---

## ✅ MENOS ES MÁS

Simplificar:
- variables
- estados
- animaciones
- triggers

Hace el sistema:
- más estable
- más escalable
- más fácil de mantener

---

# 🚀 OBJETIVO ACTUAL

Construir una base sólida de combate 2.5D:
- estable
- escalable
- reutilizable
- profesional

ANTES de agregar:
- contenido
- efectos
- online
- personajes masivos

---

# 📌 ESTADO GENERAL

## 🔥 AVANCE ACTUAL:
MUY BUENO.

Ya no estás haciendo:
- scripts básicos

Ahora estás haciendo:
- ARQUITECTURA DE GAMEPLAY.

Y eso es desarrollo real de videojuegos.
