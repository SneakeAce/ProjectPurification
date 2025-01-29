using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        Time.timeScale = 0f;

        Application.Quit();
    }
}
