using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���̊Ǘ����s�����߂ɕK�v


public class GameManager : MonoBehaviour
{
    public string labelText = "4�̃A�C�e�����W�߂Ď��R��������낤�I";    // ���x���ɕ\�����镶����
    public int maxItems = 4;            // �A�C�e���̑���
    public bool showWinScreen = false;  // �Q�[���N���A��ʂ�\�����邩�ǂ���
    public bool showLossScreen = false; // �Q�[���I�[�o�[��ʂ�\�����邩�ǂ���

    private int _itemsCollected = 0;    // �v���C���[���W�߂��A�C�e���̐�
    // _itemsCollected���v���p�e�B�Ƃ��Ē�`����
    public int Items
    {
        get
        {
            return _itemsCollected;
        }
        set
        {
            _itemsCollected = value;
            Debug.Log("Items: " + _itemsCollected);

            if (_itemsCollected >= maxItems)
            {
                labelText = "���ׂẴA�C�e�����W�߂��I";
                showWinScreen = true;
                Time.timeScale = 0;     // �Q�[���̎��Ԃ��~����
            }
            else
            {
                labelText = "�A�C�e�����������ˁB���ƁA" + (maxItems - _itemsCollected) + "����I";
            }
        }
    }

    private int _playerHP = 10;         // �v���C���[�̗̑�
    // _playerHP���v���p�e�B�Ƃ��Ē�`����
    public int HP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            _playerHP = value;
            Debug.Log("HP: " + _playerHP);

            if (_playerHP <= 0)
            {
                labelText = "�Q�[���I�[�o�[�I";
                showLossScreen = true;
                Time.timeScale = 0;     // �Q�[���̎��Ԃ��~����
            }
            else
            {
                labelText = "�̗͂���������I";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        // �v���C���[��HP�\�� Rect(x���W, y���W, ��, ����)
        GUI.Box(new Rect(20, 20, 150, 25), "Player HP: " + _playerHP);
        // �W�߂��A�C�e�����̕\��
        GUI.Box(new Rect(20, 50, 150, 25), "Items: " + _itemsCollected);
        // ���x���̕\��
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        // �Q�[���N���A��ʂ̕\��
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "�Q�[���N���A�I"))
            {
                RestartGame();
            }
        }

        // �Q�[���I�[�o�[��ʂ̕\��
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "�Q�[���I�[�o�[�I"))
            {
                RestartGame();
            }
        }

        // �Q�[�����X�^�[�g
        void RestartGame()
        {
            // �Q�[���N���A�{�^�����N���b�N������A�Q�[�����ăX�^�[�g����
            SceneManager.LoadScene("GameScene");  // �V�[���ؑցi�ēǂݍ��݁j
            Time.timeScale = 1;     // �Q�[���̎��Ԃ��ĊJ����
        }
    }
}
