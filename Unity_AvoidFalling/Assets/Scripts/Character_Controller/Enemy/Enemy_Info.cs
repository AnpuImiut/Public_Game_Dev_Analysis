using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Info : MonoBehaviour
{
    [SerializeField] private string id;

    public string get_id() {return id;}
}
