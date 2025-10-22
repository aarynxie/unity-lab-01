using UnityEngine;
using UnityEngine.Events;

public class RestartButtonController : MonoBehaviour, IInteractiveButton
{
    public UnityEvent gameRestart;
    public void ButtonClick()
    {
        Debug.Log("Onclick restart button");
        //GameManager.instance.GameRestart();
        gameRestart.Invoke();
    }
}
