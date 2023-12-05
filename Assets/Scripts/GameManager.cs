using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーンの管理を行うために必要


public class GameManager : MonoBehaviour
{
    public string labelText = "4つのアイテムを集めて自由を勝ち取ろう！";    // ラベルに表示する文字列
    public int maxItems = 4;            // アイテムの総数
    public bool showWinScreen = false;  // ゲームクリア画面を表示するかどうか
    public bool showLossScreen = false; // ゲームオーバー画面を表示するかどうか

    private int _itemsCollected = 0;    // プレイヤーが集めたアイテムの数
    // _itemsCollectedをプロパティとして定義する
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
                labelText = "すべてのアイテムを集めた！";
                showWinScreen = true;
                Time.timeScale = 0;     // ゲームの時間を停止する
            }
            else
            {
                labelText = "アイテムを見つけたね。あと、" + (maxItems - _itemsCollected) + "つだよ！";
            }
        }
    }

    private int _playerHP = 10;         // プレイヤーの体力
    // _playerHPをプロパティとして定義する
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
                labelText = "ゲームオーバー！";
                showLossScreen = true;
                Time.timeScale = 0;     // ゲームの時間を停止する
            }
            else
            {
                labelText = "体力が減ったよ！";
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
        // プレイヤーのHP表示 Rect(x座標, y座標, 幅, 高さ)
        GUI.Box(new Rect(20, 20, 150, 25), "Player HP: " + _playerHP);
        // 集めたアイテム数の表示
        GUI.Box(new Rect(20, 50, 150, 25), "Items: " + _itemsCollected);
        // ラベルの表示
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        // ゲームクリア画面の表示
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "ゲームクリア！"))
            {
                RestartGame();
            }
        }

        // ゲームオーバー画面の表示
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "ゲームオーバー！"))
            {
                RestartGame();
            }
        }

        // ゲームリスタート
        void RestartGame()
        {
            // ゲームクリアボタンをクリックしたら、ゲームを再スタートする
            SceneManager.LoadScene("GameScene");  // シーン切替（再読み込み）
            Time.timeScale = 1;     // ゲームの時間を再開する
        }
    }
}
