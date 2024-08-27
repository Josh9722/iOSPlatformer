using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScroller : MonoBehaviour
{

    [SerializeField] public static int factor = 8;
    public float speed;

    public Vector3 dir = Vector3.down; 


    public void Start()
    {

    }
    void Update()
    {
        transform.position += dir * Time.deltaTime * speed * factor;

        if (transform.position.y < - 19) {
            this.transform.position = new Vector3(this.transform.position.x, 19f, this.transform.position.z);

        }

    }

}
