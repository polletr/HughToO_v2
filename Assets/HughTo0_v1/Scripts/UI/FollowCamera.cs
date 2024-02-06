using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera mainCamera; // Arraste a c�mera desejada para esta vari�vel no Inspector
    public RectTransform uiPanel; // Arraste o objeto UI para esta vari�vel no Inspector

    void Update()
    {
        if (mainCamera != null && uiPanel != null)
        {
            // Obter a posi��o da c�mera no mundo
            Vector3 cameraPosition = mainCamera.transform.position;

            // Configurar a posi��o do UI para seguir a c�mera
            uiPanel.position = new Vector3(cameraPosition.x, cameraPosition.y, uiPanel.position.z);
        }
    }
}