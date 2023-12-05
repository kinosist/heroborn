using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameManager gameManager;     // GameManager�I�u�W�F�N�g���i�[����ϐ�

    // Start is called before the first frame update
    void Start()
    {
        // GameManager�I�u�W�F�N�g��T���āAGameManager�R���|�[�l���g���擾����
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnCollisionEnter()�́A���̃I�u�W�F�N�g�ɏՓ˂����u�ԂɌĂяo�����֐�
    // ������collision�ɂ́A�Փ˂�������̏�񂪓����Ă���
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item Get!");
            // GameManager�I�u�W�F�N�g��Items�v���p�e�B��1���₷
            gameManager.Items++;
        }
    }
}
