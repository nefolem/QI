using System;
using Unity.VisualScripting;
using UnityEngine;

public class RotateAndMove : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 30f; 
    [SerializeField] private float _moveSpeed = 1f; 
    [SerializeField] private float _moveDistance = 1f; 

    private Vector3 _startPosition; 

    void Start()
    {
        _startPosition = transform.position; 
    }

    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

        float newY = _startPosition.y + Mathf.Sin(Time.time * _moveSpeed) * _moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instanсe.AddHeart();
            Destroy(gameObject);
        }
    }
}