using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syougaibutu : MonoBehaviour
{
     //敵プレハブ
    public GameObject[] step = new GameObject[10];
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0.0f;
    //Z座標の最小値
    public float zMinPosition = 0f;
    //Z座標の最大値
    public float zMaxPosition = 3f;

    public float speed = -10f;

    private float posCount;
    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = 1f;
        time = -3f;
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;
 
        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if(time > interval - Time.deltaTime)
        {
            //enemyをインスタンス化する(生成する)
            for (int i=0; i<2; i++){
                GameObject enemy = Instantiate(step[Random.Range(0,10)]);
                Enemy e = enemy.GetComponent<Enemy>();
                e.speed = 10;
                //生成した敵の位置をランダムに設定する
                enemy.transform.position = new Vector3(20,0.8f,0);
                Rigidbody rg = enemy.GetComponent<Rigidbody>();
                
                posCount = enemy.gameObject.transform.position.z;
            } 
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
        }
    }
}
