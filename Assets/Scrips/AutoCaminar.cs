using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AutoCaminar : MonoBehaviour
{
    public GameObject VisionVR;
    public const int AnguloRecto = 90;
    public bool EstaCaminando = false;
    public float Velocidad;
    public bool CaminarCuanddoPulsamos;
    public bool CaminarCuandoMiramos;
    public double AnguloDelUmbral;
    public bool CongelarLaPosicionY;
    public float CompensarY;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            Debug.LogError("Falta el CharacterController en el GameObject");
    }

    void Update()
    {
        if (CaminarCuandoMiramos && !CaminarCuanddoPulsamos && !EstaCaminando && VisionVR.transform.eulerAngles.x >= AnguloDelUmbral && VisionVR.transform.eulerAngles.x <= AnguloRecto)
        {
            EstaCaminando = true;
        }
        else if (CaminarCuandoMiramos && !CaminarCuanddoPulsamos && EstaCaminando && (VisionVR.transform.eulerAngles.x <= AnguloDelUmbral || VisionVR.transform.eulerAngles.x >= AnguloRecto))
        {
            EstaCaminando = false;
        }

        if (EstaCaminando)
        {
            Caminar();
        }

        if (CongelarLaPosicionY)
        {
            Vector3 pos = transform.position;
            pos.y = CompensarY;
            transform.position = pos;
        }
    }

    private void Caminar()
    {
        // Dirección solo horizontal, sin componente Y
        Vector3 direccion = new Vector3(VisionVR.transform.forward.x, 0, VisionVR.transform.forward.z).normalized;

        // Multiplicamos por velocidad y deltaTime
        Vector3 movimiento = direccion * Velocidad * Time.deltaTime;

        // Mover con CharacterController para que respete colisiones y suelo
        controller.Move(movimiento);
    }
}
