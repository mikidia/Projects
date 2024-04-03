using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTriger : MonoBehaviour
{
    
    [SerializeField]SoundManager soundManager;
    public static   WinTriger instance;

    private void Start ()
    {
        soundManager = SoundManager.instance;
        if (instance == null)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {

            LoadNextScene();


        }
    }
    public void LoadNextScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }



}
