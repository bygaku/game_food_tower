using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] characters_;      /// @brief Character Object Array
    GameObject generate_character_;                 /// @brief Generated Character

    bool is_generate_     = false;                  /// @brief 生成されたか判定
    bool is_interval_     = false;                  /// @brief キャラクター生成の間隔を制御
    bool is_button_hover_ = false;                  /// @brief ボタンがホバーされているか
    bool is_game_started_ = false;                  /// @brief ゲーム開始状態を管理

    public static bool is_game_over_ = false;       /// @brief ゲームオーバー判定

    int score_;                                     /// @brief スコアを管理
    [SerializeField] TextMeshProUGUI score_text_;   /// @brief スコア表示用のTextMeshProUGUI

    // Start is called before the first frame update
    void Start() {
        is_game_over_    = false;
        is_generate_     = false;
        is_interval_     = false;
        is_button_hover_ = false;
        is_game_started_ = false;

        score_ = 0;
        score_text_.text = score_.ToString();
    }

    // Update is called once per frame
    void Update() {
        if (is_game_over_) return;

        //! キャラクターが生成されていない、静止している、且つインターバルでないとき
        if (!is_generate_ && !is_interval_ && !CheckMove()) {
            CreateCharacter();
            is_generate_ = true;

            if  (is_game_started_) UpdateScore();
            else is_game_started_ = true; //ゲーム開始状態を更新
        }
        //! マウスの左ボタンが放された時、且つキャラクターが生成されている場合   
        else if (Input.GetMouseButtonUp(0) && is_generate_ && !is_button_hover_) {
            generate_character_.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            is_generate_ = false;
            StartCoroutine(IntervalCoroutine());
        }
        else if (Input.GetMouseButton(0) && is_generate_ && !is_button_hover_)
        {
            //マウスの左ボタンが押されている間、キャラクターを移動させる(x座標のみ)
            float mouse_pos_x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            generate_character_.transform.position = new Vector2(mouse_pos_x, transform.position.y);
        }
    }
    
    void CreateCharacter()
    {
        generate_character_ = Instantiate(characters_[Random.Range(0, characters_.Length)],
            transform.position, Quaternion.identity);
     
        generate_character_.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    IEnumerator IntervalCoroutine()
    {
        is_interval_ = true;
        yield return new WaitForSeconds(1f);
        is_interval_ = false;
    }

    bool CheckMove()
    {
        //! Characterタグのオブジェクトを取得
        GameObject[] character_objects = GameObject.FindGameObjectsWithTag("Character");
        
        foreach (GameObject character in character_objects) {
            //! キャラクターの速度が0.001以上なら動いていると判断
            if (character.GetComponent<Rigidbody2D>().velocity.magnitude > 0.001f) {
                return true;
            }
        }

        return false;
    }

    public void RotateCharacter()
    {
        if (is_generate_) generate_character_.transform.Rotate(0, 0, -30);
    }

    public void IsButtonChange(bool is_x)
    {
        is_button_hover_ = is_x; //ボタンの状態を変更
    }

    void UpdateScore()
    {
        score_++;
        score_text_.text = score_.ToString();
    }
}