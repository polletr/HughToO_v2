using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera mainCamera; // Arraste a câmera desejada para esta variável no Inspector
    public RectTransform uiPanel; // Arraste o objeto UI para esta variável no Inspector

    void Update()
    {
        if (mainCamera != null && uiPanel != null)
        {
            // Obter a posição da câmera no mundo
            Vector3 cameraPosition = mainCamera.transform.position;

            // Configurar a posição do UI para seguir a câmera
            uiPanel.position = new Vector3(cameraPosition.x, cameraPosition.y, uiPanel.position.z);
        }
    }
}