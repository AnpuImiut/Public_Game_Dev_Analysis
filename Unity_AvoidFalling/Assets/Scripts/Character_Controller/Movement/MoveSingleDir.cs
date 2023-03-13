using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MoveSingleDir : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(dir * Time.deltaTime * speed, Space.Self);
    }

}
