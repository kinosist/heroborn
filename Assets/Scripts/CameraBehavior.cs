using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f); // カメラの位置を調整するための変数

    private Transform target; // プレイヤーのTransformコンポーネントを入れる変数

    // Start is called before the first frame update
    void Start()
    {
        // Playerという名前のついたオブジェクトのTransformコンポーネントを取得して変数に入れる
        target = GameObject.Find("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // LateUpdate()はUpdate()の後に実行される
    void LateUpdate()
    {
        // カメラの位置をプレイヤーの位置に合わせて移動させる
        this.transform.position = target.TransformPoint(camOffset);
        // カメラがプレイヤーを見るようにする
        this.transform.LookAt(target);
    }
}
