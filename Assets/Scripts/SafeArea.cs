using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] GameObject resultPanel;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character")) {
            if (!GameManager.is_game_over_) {
                GameOver();
            }

            Destroy(collision.gameObject);
        }
    }

    void GameOver()
    {
        resultPanel.SetActive(true);        //! ���ʃp�l����\��
        GameManager.is_game_over_ = true;       //! �Q�[���I�[�o�[��Ԃ��X�V
    }

}
