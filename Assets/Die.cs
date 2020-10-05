using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public GameObject Gameover;
    void Update()
    {
        if(transform.position.y <-2) {
            Gameover.SetActive(true);
        }
    }
}
