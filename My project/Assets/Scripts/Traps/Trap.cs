using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float rotateSpeed = 1;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform playerObject;
    [SerializeField] Vector3 finalCords;
    [SerializeField] bool isReturning;
    [SerializeField] bool isMoving;
    [SerializeField] bool isHorizontal;
    [SerializeField] Transform startPos,finalPos;
    [NonSerialized] GameManager gameManager;



    public void Awake ()
    {

        gameManager = GameManager.instance;

    }


    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(0, 0, rotateSpeed);
        if (isMoving)
        {
            Move();
        }
    }
    void Move ()
    {
        if (isReturning&& isHorizontal)
        {
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        if (!isReturning&& isHorizontal)
        {
            transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        if (isReturning && !isHorizontal)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime),  transform.position.z);
        }
        if (!isReturning && !isHorizontal)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
        }


        if (Vector3.Distance(finalPos.transform.position, transform.position) <= 0.1f)
        {
            isReturning = true;


        }
        if (Vector3.Distance(startPos.transform.position, transform.position) <= 0.1f)
        {

            isReturning = false;

        }



    }


    
    public void OnTriggerEnter2D (Collider2D collision)
    {
        print("asdasd");

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Death();
            gameManager.Light();


        }

    }





}
