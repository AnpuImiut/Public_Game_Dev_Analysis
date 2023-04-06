using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MoveSingleDir : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 dir;

    void Update()
    {
        gameObject.transform.Translate(dir * Time.deltaTime * speed, Space.Self);
    }

}
