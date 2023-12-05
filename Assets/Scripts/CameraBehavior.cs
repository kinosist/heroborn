using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f); // �J�����̈ʒu�𒲐����邽�߂̕ϐ�

    private Transform target; // �v���C���[��Transform�R���|�[�l���g������ϐ�

    // Start is called before the first frame update
    void Start()
    {
        // Player�Ƃ������O�̂����I�u�W�F�N�g��Transform�R���|�[�l���g���擾���ĕϐ��ɓ����
        target = GameObject.Find("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // LateUpdate()��Update()�̌�Ɏ��s�����
    void LateUpdate()
    {
        // �J�����̈ʒu���v���C���[�̈ʒu�ɍ��킹�Ĉړ�������
        this.transform.position = target.TransformPoint(camOffset);
        // �J�������v���C���[������悤�ɂ���
        this.transform.LookAt(target);
    }
}
