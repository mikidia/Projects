using System;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float rotateSpeed = 1;
    //EffectManager EffectsSustem;
    //public GameObject bloodstains;
    [SerializeField] Transform playerObject;
    //private GameObject blood;
    [SerializeField] int trapDamage = 0;
    [SerializeField] bool isMoving;
    [SerializeField] float moveSpeed;
    [SerializeField]bool isReturning;
    [SerializeField] Vector3 finalCords;
    [NonSerialized] Vector3 startCords;



    public void Awake ()
    {
        startCords = transform.position;
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
        if (isReturning)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        }
        if (!isReturning)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);


        }


        if (Vector3.Distance(finalCords, transform.position) <= 0.5f)
        {
            isReturning = true;


        }
        Debug.Log(Vector3.Distance(finalCords, transform.position) );
        if (Vector3.Distance(startCords, transform.position) <= 0.5f)
        {

            isReturning = false;

        }



    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.GetDamage(trapDamage);
        }

    }
}
