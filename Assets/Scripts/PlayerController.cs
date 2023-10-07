using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    private int numPickup = 6;
    private int count = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    private void SetCountText()
    {
        scoreText.text = "Score : " + count.ToString();
        if (count >= numPickup)
        {
            winText.text = "You Win";
        }
    }

    private void Start()
    {
        SetCountText();
        winText.text = "";
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x,0.0f,moveValue.y);

        GetComponent<Rigidbody>().AddForce(movement*speed*Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText() ;
        }
    }
}
