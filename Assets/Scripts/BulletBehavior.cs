using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;    // �e����ʂɕ\������Ă��鎞��

    // Start is called before the first frame update
    void Start()
    {
        // ��ʂɕ\������Ă��鎞�Ԃ��o�߂�����A�e���폜����
        Destroy(this.gameObject, onscreenDelay);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
