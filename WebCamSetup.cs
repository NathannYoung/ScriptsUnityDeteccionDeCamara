using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamSetup : MonoBehaviour
{
    WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Start()
    {
        // Verificar si el usuario ha autorizado el acceso a la c�mara
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("Permiso concedido para acceder a la c�mara.");
        }
        else
        {
            Debug.LogError("Permiso no concedido para acceder a la c�mara.");
            // Solicitar autorizaci�n si no tiene permiso
            StartCoroutine(RequestCameraPermission());
            return;
        }

        // Obtener la lista de dispositivos de la c�mara disponibles
        WebCamDevice[] devices = WebCamTexture.devices;

        // Comprobar si hay dispositivos de c�mara disponibles
        if (devices.Length == 0)
        {
            Debug.LogError("No se encontraron c�maras disponibles.");
            return;
        }

        // Mostrar los nombres de las c�maras disponibles
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("C�mara " + i + ": " + devices[i].name);
        }

        // Asumir que la primera c�mara es la correcta, pero puedes seleccionar otra seg�n lo necesites
        WebCamDevice selectedDevice = devices[1];
        webcamTexture = new WebCamTexture(selectedDevice.name);

        // Configurar la textura de la c�mara
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

        // Iniciar la c�mara
        webcamTexture.Play();
        Debug.Log("C�mara iniciada correctamente.");
    }

    // Solicitar permisos para la c�mara
    IEnumerator RequestCameraPermission()
    {
        Debug.Log("Solicitando permiso para la c�mara...");
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("Permiso concedido para la c�mara.");
        }
        else
        {
            Debug.LogError("Permiso no concedido para la c�mara.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Aqu� podr�as agregar l�gica adicional, si lo necesitas
    }
}
