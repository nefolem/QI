using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ButtonNo : MonoBehaviour
{
    [SerializeField] private List<Transform> _movePoints;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = _movePoints[Random.Range(0, _movePoints.Count)].position;
            print("no");
        }
    }
}
