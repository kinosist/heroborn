using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBihevior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;   // 地面と判定する距離
    public LayerMask groundLayer;       // 画面側でGroundレイヤーを選択する
    public GameObject bullet;           // 弾のプレハブを入れる変数
    public float bulletSpeed = 100f;    // 弾の速度

    private float vInput;   // 上下方向の入力状態を入れる変数
    private float hInput;   // 左右方向の入力状態を入れる変数
    private Rigidbody _rb;  // Rigidbodyコンポーネントを入れる変数
    private CapsuleCollider _col;   // CapsuleColliderコンポーネントを入れる変数
    private GameManager _gameManager;   // GameManagerスクリプトを入れる変数

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyコンポーネントを取得して変数に入れる
        _rb = GetComponent<Rigidbody>();
        // CapsuleColliderコンポーネントを取得して変数に入れる
        _col = GetComponent<CapsuleCollider>();
        // GameManagerスクリプトを取得して変数に入れる
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Input.GetAxis("Vertical")は上下方向の入力状態を取得する
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        // Input.GetAxis("Horizontal")は左右方向の入力状態を取得する
        hInput = Input.GetAxis("Horizontal") * rotationSpeed;

        // ジャンプ
        // ユーザーの入力検知は必ずUpdate()に入れること
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            // ForceMode.Impulseは、瞬間的に力を加える
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        // 弾を発射する
        if (Input.GetMouseButtonDown(0))    // マウスの左クリックが押されたとき
        {
            // 弾のプレハブを元に、弾を生成する
            GameObject bullets = Instantiate(
                bullet, 
                this.transform.position + new Vector3(1, 0, 0), 
                this.transform.rotation
                ) as GameObject;
            // Rigidbodyコンポーネントを取得
            Rigidbody bulletRb = bullets.GetComponent<Rigidbody>();
            // 弾を飛ばす
            bulletRb.velocity = this.transform.forward * bulletSpeed;
        }
        
    }

    // 物理演算の処理はFixedUpdate()内で行う
    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput; // 回転量を入れる変数
        // 回転量をQuaternionに変換する
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime); 
        // 前進する
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        // 回転する
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    // 地面との判定を行う
    private bool IsGrounded()
    {
        // カプセルコライダーの中心から、カプセルコライダーの底面までの距離を計算する
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        // カプセルコライダーの底面から地面までの距離を計算する
        bool grounded = Physics.CheckCapsule(
            _col.bounds.center,     // カプセルコライダーの中心
            capsuleBottom,          // カプセルコライダーの底面
            distanceToGround,       // 地面と判定する距離
            groundLayer,            // 地面と判定するレイヤー
            QueryTriggerInteraction.Ignore      // Triggerとの判定は無視する
            );

        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 敵と衝突したら、HPを減らす
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }
}
