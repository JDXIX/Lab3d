# Lab3D – Entornos y Programación 3D

Proyecto de laboratorio desarrollado como práctica de entornos y programación 3D en Unity.

---

## Descripción

**Lab3D** es un laboratorio interactivo de exploración en un entorno 3D abierto.
El jugador controla a **Guardian3D**, un personaje navegable en un terreno tridimensional
con movimiento completo, animaciones fluidas y sistema de salto.

El entorno presenta una ambientación de exploración con iluminación cálida y niebla ambiental
para generar profundidad visual.

---

## Personaje

- **Nombre:** Guardian3D
- **Tipo de rig:** Humanoid (Unity Animator)
- **Controlador de animación:** Animacion.controller (Blend Tree 2D + Standing Jump)

---

## Controles del juego

| Tecla | Acción |
|-------|--------|
| `W` / `↑` | Avanzar (correr hacia adelante) |
| `S` / `↓` | Retroceder |
| `A` / `←` | Desplazarse a la izquierda (strafe) |
| `D` / `→` | Desplazarse a la derecha (strafe) |
| `X` | Saltar |

---

## Acciones implementadas

- ✅ **Quieto** – el personaje permanece en animación Idle cuando no hay entrada
- ✅ **Correr** – movimiento hacia adelante con animación de carrera (Fast Run)
- ✅ **Correr a la izquierda** – strafe izquierdo (Left Strafe)
- ✅ **Correr a la derecha** – strafe derecho (mirrored Left Strafe)
- ✅ **Saltar** – salto con física (Rigidbody + Impulse) y animación Standing Jump
- ✅ **Caer** – caída natural por gravedad, regreso a Idle al tocar el suelo

---

## Estructura del proyecto

```
Assets/
├── Animaciones/          # Animator Controller y clips .anim
│   ├── Animacion.controller
│   ├── Idle.anim
│   ├── Fast Run.anim
│   ├── Left Strafe.anim
│   └── Standing Jump.anim
├── Modelo/               # Modelos 3D del personaje
│   └── Ch39_nonPBR.fbx   (modelo  Mixamo – Humanoid)
├── Scripts/
│   ├── Movimiento.cs     # Control del jugador (WASD + salto)
│   └── SeguirCamara.cs   # Cámara que sigue al personaje
├── Scenes/
│   ├── SampleScene.unity         # Escena principal activa
└── Textura/              # Texturas del terreno
```

---

## Cómo abrir y probar en Unity

1. Abrir **Unity Hub**
2. Añadir el proyecto: seleccionar la carpeta `Lab3d/`
3. Abrir con **Unity 6** (o la versión del proyecto)
4. En el panel **Project**, abrir `Assets/Scenes/SampleScene`
5. Presionar **Play** ▶
6. Usar las teclas WASD o flechas para mover a Guardian3D
7. Presionar **X** para saltar

> **Nota:** Si se desea usar el modelo Ch39_nonPBR como personaje visual,
> ir a `Assets/Modelo/Ch39_nonPBR.fbx` → Inspector → Rig → Animation Type: **Humanoid** → Apply.
> Luego reemplazar el modelo visual hijo dentro del GameObject Guardian3D.

---


*Laboratorio de Entornos y Programación 3D – Proyecto Lab3D*
