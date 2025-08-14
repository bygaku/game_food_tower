using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //シーンを切り替えるメソッド
    public void ChangeToScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}