using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class ControllerVRPlayer : MonoBehaviour
{
    private CharacterController ControladorDelPersonaje;
    private Vector3 MovimientoEnDireccion = Vector3.zero;
    private Vector2 Entrada;

    private CollisionFlags BanderasDeColision;

    public float FuerzaAlTocarElSuelo;
    public float MultiplicarGravedad;
    void Start()
    {
        ControladorDelPersonaje = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 MovimientoDeseado = transform.forward * Entrada.y + transform.right * Entrada.x;

        RaycastHit histInfo;
        Physics.SphereCast(transform.position, ControladorDelPersonaje.radius, Vector3.down, out histInfo, 
                    ControladorDelPersonaje.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        MovimientoDeseado = Vector3.ProjectOnPlane(MovimientoDeseado, histInfo.normal).normalized;

        if (ControladorDelPersonaje.isGrounded)
        {
            MovimientoEnDireccion.y = -FuerzaAlTocarElSuelo;
        }
        else
        {
            MovimientoEnDireccion += Physics.gravity * MultiplicarGravedad * Time.fixedDeltaTime;
        }

        BanderasDeColision = ControladorDelPersonaje.Move(MovimientoEnDireccion * Time.fixedDeltaTime);
    }
}
