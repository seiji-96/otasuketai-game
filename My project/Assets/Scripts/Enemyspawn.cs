using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawn : MonoBehaviour
{
     //敵プレハブ
    public GameObject enemyPrefab;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0.0f;
    //Z座標の最小値
    public float zMinPosition = 0f;
    //Z座標の最大値
    public float zMaxPosition = 3f;

    public float speed = -10f;
    private float disappear = -3;

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
                GameObject enemy = Instantiate(enemyPrefab);
                Enemy e = enemy.GetComponent<Enemy>();
                e.speed = 10;
                //生成した敵の位置をランダムに設定する
                enemy.transform.position = GetRandomPosition();
                Rigidbody rg = enemy.GetComponent<Rigidbody>();
                if (enemy.gameObject.transform.position.x < disappear || posCount == enemy.gameObject.transform.position.z)
                {
                    Destroy(enemy);
                }
                posCount = enemy.gameObject.transform.position.z;
            } 
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
        }
    }
    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = 20f;
        float y = 0.25f;
        int num = Random.Range(1, 4);
        float z;
        if(num == 1)
        {
            z = -1.0f;
        }else if(num == 2){
            z = 0;
        }else{
            z = 1.0f;
        }
 
        //Vector3型のPositionを返す
        return new Vector3(x,y,z);
    }

}
