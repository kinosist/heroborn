using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;    // 弾が画面に表示されている時間

    // Start is called before the first frame update
    void Start()
    {
        // 画面に表示されている時間が経過したら、弾を削除する
        Destroy(this.gameObject, onscreenDelay);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
