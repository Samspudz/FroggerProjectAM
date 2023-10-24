using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCycleScript : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right;
    public float moveSpeed = 1;

    public float objSize = 1;

    [SerializeField] private Vector3 leftEdge;
    [SerializeField] private Vector3 rightEdge;

    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    void Update()
    {
        if (moveDirection.x > 0 && (transform.position.x - objSize) > rightEdge.x)
        {
            Vector3 _position = transform.position;
            _position.x = leftEdge.x - objSize;
            transform.position = _position;
        }

        else if(moveDirection.x < 0 && (transform.position.x + objSize) < leftEdge.x)
        {
            Vector3 _position = transform.position;
            _position.x = rightEdge.x + objSize;
            transform.position = _position;
        }

        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
