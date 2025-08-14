using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void RetryGame()
    {
        //! Œ»İ‚ÌƒV[ƒ“‚ğÄ“Ç‚İ‚İ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}