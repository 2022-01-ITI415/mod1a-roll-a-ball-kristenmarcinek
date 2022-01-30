using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public GameObject winTextObject;
    public GameObject timeTextObject;

    private Rigidbody rb;
    private int count;
    private double timer;
    private float movementX;
    private float movementY;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        timer = Time.realtimeSinceStartup;

        SetCountText();
        SetTimeText();
        winTextObject.SetActive(false);
        timeTextObject.SetActive(false);
    }

    // Player Input
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetTimeText()
    {
        timeText.text = "Time: " + timer.ToString();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 12)
        {
            winTextObject.SetActive(true);
            timeTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
            audioSource.Play();
        }
    }

}
