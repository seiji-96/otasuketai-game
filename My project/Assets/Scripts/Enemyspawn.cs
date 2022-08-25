using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawn : MonoBehaviour
{
    public GameObject enemy;

    GameObject[] step = new GameObject[5];
    public float speed = 10;
    private float disappear = -10;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < step.Length; i++)
        {
            int n = Random.Range(1, 4);
            step[i] = Instantiate(enemy, new Vector3(20f, 0.25f, 0.5f * n), Quaternion.identity);
            step[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (step[i].gameObject.transform.position.x < disappear)
            {
                Destroy(step[i].gameObject);
            }
        }
    }

}
