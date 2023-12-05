using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform patrolRoute;       // �G�����񂷂郋�[�g�̐e�I�u�W�F�N�g
    public List<Transform> locations;   // ���񃋁[�g�̎q�I�u�W�F�N�g�̈ʒu���
    public Transform player;            // �v���C���[

    private int locationIndex = 0;      // ���񃋁[�g�̎q�I�u�W�F�N�g�̈ʒu���̃C���f�b�N�X
    private NavMeshAgent agent;         // NavMeshAgent�R���|�[�l���g���i�[����ϐ�
    private int _lives = 3;             // �G�̗̑�

    // _lives���v���p�e�B�Ƃ��Ē�`����
    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("�G��|�����I");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveNextPatrolLocation();
    }

    void InitializePatrolRoute()
    {
        // patrolRoute�̎q�I�u�W�F�N�g��lications�ɒǉ�����
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            // ���񃋁[�g�̎q�I�u�W�F�N�g���ݒ肳��Ă��Ȃ��ꍇ�́A�������I������
            return;
        }

        // Enemy�̌������ʒu��ݒ肷��
        agent.destination = locations[locationIndex].position;

        // ���񃋁[�g�̎q�I�u�W�F�N�g�̈ʒu���̃C���f�b�N�X���X�V����
        locationIndex = (locationIndex + 1) % locations.Count;
        // 0 + 1 % 4 = 1
        // 1 + 1 % 4 = 2
        // 2 + 1 % 4 = 3
        // 3 + 1 % 4 = 0
        // 0 + 1 % 4 = 1
        //if (locationIndex <= 3)
        //{
        //    locationIndex += 1;
        //}
        //else
        //{
        //    locationIndex = 0;
        //}

    }

    // Update is called once per frame
    void Update()
    {
        // �G�[�W�F���g�����݂̖ړI�n�ɓ���������A���̏���n�_��ݒ肷��
        // agent.remainingDistance�͖ړI�n����̋���
        // agent.pathPending�͖ړI�n�ւ̌o�H���v�Z�����ǂ���
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveNextPatrolLocation();
        }
        
    }

    // �g���K�[�ɓ��������ɌĂяo�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("�v���C���[���m�I");
            agent.destination = player.position;    // �v���C���[�Ɍ����킹��
        }        
    }

    // �g���K�[����o�����ɌĂяo�����
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("�v���C���[�����ꂽ�I");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // prefab������ꂽ�I�u�W�F�N�g�͖��O��(Clone)���t��
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            // �e���폜����
            Destroy(collision.gameObject);
            // �G�̗̑͂����炷
            Lives -= 1;
            Debug.Log("�����I");
        }        
    }

}
