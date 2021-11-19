using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attachToPlayer()
    {
        Transform carTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        transform.SetParent(carTransform);
        transform.RotateAround(transform.position, Vector3.right, 180);
        ps.Clear();
    }
}
