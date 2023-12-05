using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform patrolRoute;       // 敵が巡回するルートの親オブジェクト
    public List<Transform> locations;   // 巡回ルートの子オブジェクトの位置情報
    public Transform player;            // プレイヤー

    private int locationIndex = 0;      // 巡回ルートの子オブジェクトの位置情報のインデックス
    private NavMeshAgent agent;         // NavMeshAgentコンポーネントを格納する変数
    private int _lives = 3;             // 敵の体力

    // _livesをプロパティとして定義する
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
                Debug.Log("敵を倒した！");
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
        // patrolRouteの子オブジェクトをlicationsに追加する
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            // 巡回ルートの子オブジェクトが設定されていない場合は、処理を終了する
            return;
        }

        // Enemyの向かう位置を設定する
        agent.destination = locations[locationIndex].position;

        // 巡回ルートの子オブジェクトの位置情報のインデックスを更新する
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
        // エージェントが現在の目的地に到着したら、次の巡回地点を設定する
        // agent.remainingDistanceは目的地からの距離
        // agent.pathPendingは目的地への経路が計算中かどうか
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveNextPatrolLocation();
        }
        
    }

    // トリガーに入った時に呼び出される
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("プレイヤー検知！");
            agent.destination = player.position;    // プレイヤーに向かわせる
        }        
    }

    // トリガーから出た時に呼び出される
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("プレイヤーが離れた！");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // prefabから作られたオブジェクトは名前に(Clone)が付く
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            // 弾を削除する
            Destroy(collision.gameObject);
            // 敵の体力を減らす
            Lives -= 1;
            Debug.Log("命中！");
        }        
    }

}
