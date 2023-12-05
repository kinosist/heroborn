using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBihevior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;   // �n�ʂƔ��肷�鋗��
    public LayerMask groundLayer;       // ��ʑ���Ground���C���[��I������
    public GameObject bullet;           // �e�̃v���n�u������ϐ�
    public float bulletSpeed = 100f;    // �e�̑��x

    private float vInput;   // �㉺�����̓��͏�Ԃ�����ϐ�
    private float hInput;   // ���E�����̓��͏�Ԃ�����ϐ�
    private Rigidbody _rb;  // Rigidbody�R���|�[�l���g������ϐ�
    private CapsuleCollider _col;   // CapsuleCollider�R���|�[�l���g������ϐ�
    private GameManager _gameManager;   // GameManager�X�N���v�g������ϐ�

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody�R���|�[�l���g���擾���ĕϐ��ɓ����
        _rb = GetComponent<Rigidbody>();
        // CapsuleCollider�R���|�[�l���g���擾���ĕϐ��ɓ����
        _col = GetComponent<CapsuleCollider>();
        // GameManager�X�N���v�g���擾���ĕϐ��ɓ����
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Input.GetAxis("Vertical")�͏㉺�����̓��͏�Ԃ��擾����
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        // Input.GetAxis("Horizontal")�͍��E�����̓��͏�Ԃ��擾����
        hInput = Input.GetAxis("Horizontal") * rotationSpeed;

        // �W�����v
        // ���[�U�[�̓��͌��m�͕K��Update()�ɓ���邱��
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            // ForceMode.Impulse�́A�u�ԓI�ɗ͂�������
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        // �e�𔭎˂���
        if (Input.GetMouseButtonDown(0))    // �}�E�X�̍��N���b�N�������ꂽ�Ƃ�
        {
            // �e�̃v���n�u�����ɁA�e�𐶐�����
            GameObject bullets = Instantiate(
                bullet, 
                this.transform.position + new Vector3(1, 0, 0), 
                this.transform.rotation
                ) as GameObject;
            // Rigidbody�R���|�[�l���g���擾
            Rigidbody bulletRb = bullets.GetComponent<Rigidbody>();
            // �e���΂�
            bulletRb.velocity = this.transform.forward * bulletSpeed;
        }
        
    }

    // �������Z�̏�����FixedUpdate()���ōs��
    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput; // ��]�ʂ�����ϐ�
        // ��]�ʂ�Quaternion�ɕϊ�����
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime); 
        // �O�i����
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        // ��]����
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    // �n�ʂƂ̔�����s��
    private bool IsGrounded()
    {
        // �J�v�Z���R���C�_�[�̒��S����A�J�v�Z���R���C�_�[�̒�ʂ܂ł̋������v�Z����
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        // �J�v�Z���R���C�_�[�̒�ʂ���n�ʂ܂ł̋������v�Z����
        bool grounded = Physics.CheckCapsule(
            _col.bounds.center,     // �J�v�Z���R���C�_�[�̒��S
            capsuleBottom,          // �J�v�Z���R���C�_�[�̒��
            distanceToGround,       // �n�ʂƔ��肷�鋗��
            groundLayer,            // �n�ʂƔ��肷�郌�C���[
            QueryTriggerInteraction.Ignore      // Trigger�Ƃ̔���͖�������
            );

        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �G�ƏՓ˂�����AHP�����炷
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }
}
