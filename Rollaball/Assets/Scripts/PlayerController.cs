using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float startTime = 20f;
    private float timeRemaining;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public AudioClip soundEffect;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        timeRemaining = startTime;

        SetCountText();
        audioSource = GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >=12)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        timeRemaining -= Time.deltaTime;

        timerText.text = "Time Remaining: " + timeRemaining.ToString("F2");

        if (timeRemaining <= 0f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();

            audioSource.PlayOneShot(soundEffect);
        }
    }
}