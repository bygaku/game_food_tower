using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //�V�[����؂�ւ��郁�\�b�h
    public void ChangeToScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}