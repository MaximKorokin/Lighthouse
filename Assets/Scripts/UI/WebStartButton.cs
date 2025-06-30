using UnityEngine;
using UnityEngine.SceneManagement;

public class WebStartButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(Constants.Scene.KinsnapHQAttack.ToString());
    }
}
