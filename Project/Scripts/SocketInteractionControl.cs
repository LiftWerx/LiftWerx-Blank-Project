using UnityEngine;

public class InteractionControl : MonoBehaviour
{
    public SocketController socketController;

    public void ToggleSocketController(bool enable)
    {
        socketController.enabled = enable;
    }
}