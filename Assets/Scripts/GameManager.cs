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

    bool is_generate_     = false;                  /// @brief �������ꂽ������
    bool is_interval_     = false;                  /// @brief �L�����N�^�[�����̊Ԋu�𐧌�
    bool is_button_hover_ = false;                  /// @brief �{�^�����z�o�[����Ă��邩
    bool is_game_started_ = false;                  /// @brief �Q�[���J�n��Ԃ��Ǘ�

    public static bool is_game_over_ = false;       /// @brief �Q�[���I�[�o�[����

    int score_;                                     /// @brief �X�R�A���Ǘ�
    [SerializeField] TextMeshProUGUI score_text_;   /// @brief �X�R�A�\���p��TextMeshProUGUI

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

        //! �L�����N�^�[����������Ă��Ȃ��A�Î~���Ă���A���C���^�[�o���łȂ��Ƃ�
        if (!is_generate_ && !is_interval_ && !CheckMove()) {
            CreateCharacter();
            is_generate_ = true;

            if  (is_game_started_) UpdateScore();
            else is_game_started_ = true; //�Q�[���J�n��Ԃ��X�V
        }
        //! �}�E�X�̍��{�^���������ꂽ���A���L�����N�^�[����������Ă���ꍇ   
        else if (Input.GetMouseButtonUp(0) && is_generate_ && !is_button_hover_) {
            generate_character_.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            is_generate_ = false;
            StartCoroutine(IntervalCoroutine());
        }
        else if (Input.GetMouseButton(0) && is_generate_ && !is_button_hover_)
        {
            //�}�E�X�̍��{�^����������Ă���ԁA�L�����N�^�[���ړ�������(x���W�̂�)
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
        //! Character�^�O�̃I�u�W�F�N�g���擾
        GameObject[] character_objects = GameObject.FindGameObjectsWithTag("Character");
        
        foreach (GameObject character in character_objects) {
            //! �L�����N�^�[�̑��x��0.001�ȏ�Ȃ瓮���Ă���Ɣ��f
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
        is_button_hover_ = is_x; //�{�^���̏�Ԃ�ύX
    }

    void UpdateScore()
    {
        score_++;
        score_text_.text = score_.ToString();
    }
}