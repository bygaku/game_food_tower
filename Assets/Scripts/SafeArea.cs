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
        resultPanel.SetActive(true);        //! 結果パネルを表示
        GameManager.is_game_over_ = true;       //! ゲームオーバー状態を更新
    }

}
