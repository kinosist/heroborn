using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameManager gameManager;     // GameManagerオブジェクトを格納する変数

    // Start is called before the first frame update
    void Start()
    {
        // GameManagerオブジェクトを探して、GameManagerコンポーネントを取得する
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnCollisionEnter()は、他のオブジェクトに衝突した瞬間に呼び出される関数
    // 引数のcollisionには、衝突した相手の情報が入っている
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item Get!");
            // GameManagerオブジェクトのItemsプロパティを1増やす
            gameManager.Items++;
        }
    }
}
