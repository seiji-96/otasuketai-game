using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawn : MonoBehaviour
{
    public List<GameObject> enemy;

    GameObject[] step = new GameObject[2];
    public float speed = 10;
    private float disappear = -10;
    private float respawn = 30;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < step.Length; i++)
        {
            step[i] = Instantiate(enemy[i], new Vector3(12 * i, 1, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < step.Length; i++)
        {
            step[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (step[i].gameObject.transform.position.x < disappear)
            {
                step[i].gameObject.transform.position = new Vector3(respawn, 1, 0);
            }
        }
    }
}
