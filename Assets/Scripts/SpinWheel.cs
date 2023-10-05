using System.Collections;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    // This integer will be shown as a slider,
    // with the range of 1 to 10 in the Inspector
    [Header("Spin Speed Slider")]
    [Range(3, 10)]
    [SerializeField] float wheelSpeed = 3f;

    Rigidbody2D rigidBody2D;
    bool isRotating = false;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.rotation = 15f;
    }

    void FixedUpdate()
    {
        if (isRotating)
        {
            rigidBody2D.rotation -= wheelSpeed;
        }
    }

    IEnumerator RotateWheel()
    {
        isRotating = true;
        // Rotate for 3 secounds
        yield return new WaitForSeconds(3f);
        isRotating = false;
    }

    public void Rotate()
    {
        // Start rotating once
        if (!isRotating) 
        { 
            StartCoroutine(RotateWheel()); 
        }
    }
}
