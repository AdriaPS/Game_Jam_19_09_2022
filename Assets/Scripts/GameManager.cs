using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent onPause;
    public UnityEvent onResume;

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        onPause?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        onResume?.Invoke();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}