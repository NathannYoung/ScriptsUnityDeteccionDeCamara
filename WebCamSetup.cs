using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamSetup : MonoBehaviour
{
    WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Start()
    {
        // Verificar si el usuario ha autorizado el acceso a la cámara
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("Permiso concedido para acceder a la cámara.");
        }
        else
        {
            Debug.LogError("Permiso no concedido para acceder a la cámara.");
            // Solicitar autorización si no tiene permiso
            StartCoroutine(RequestCameraPermission());
            return;
        }

        // Obtener la lista de dispositivos de la cámara disponibles
        WebCamDevice[] devices = WebCamTexture.devices;

        // Comprobar si hay dispositivos de cámara disponibles
        if (devices.Length == 0)
        {
            Debug.LogError("No se encontraron cámaras disponibles.");
            return;
        }

        // Mostrar los nombres de las cámaras disponibles
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("Cámara " + i + ": " + devices[i].name);
        }

        // Asumir que la primera cámara es la correcta, pero puedes seleccionar otra según lo necesites
        WebCamDevice selectedDevice = devices[1];
        webcamTexture = new WebCamTexture(selectedDevice.name);

        // Configurar la textura de la cámara
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = webcamTexture;
        }
        else
        {
            Debug.LogError("El componente Renderer no se encuentra en este GameObject.");
            return;
        }

        // Iniciar la cámara
        webcamTexture.Play();
        Debug.Log("Cámara iniciada correctamente.");
    }

    // Solicitar permisos para la cámara
    IEnumerator RequestCameraPermission()
    {
        Debug.Log("Solicitando permiso para la cámara...");
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("Permiso concedido para la cámara.");
        }
        else
        {
            Debug.LogError("Permiso no concedido para la cámara.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Aquí podrías agregar lógica adicional, si lo necesitas
    }
}
