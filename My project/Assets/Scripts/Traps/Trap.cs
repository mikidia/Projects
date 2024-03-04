using UnityEngine;

public class Trap : MonoBehaviour
    {
    [Header("Settings")]
    public float rotateSpeed = 1;
    //EffectManager EffectsSustem;
    //public GameObject bloodstains;
    public Transform playerObject;
    //private GameObject blood;
    public int trapDamage = 0;





    // Update is called once per frame
    void Update ()
        {
        transform.Rotate(0, 0, rotateSpeed);
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
