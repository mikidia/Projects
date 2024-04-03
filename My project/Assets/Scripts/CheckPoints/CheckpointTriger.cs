using System;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class CheckpointTriger : MonoBehaviour
{


    #region Declarations 
    [Header("Settings")]
    [SerializeField]Transform player;
    [SerializeField]public int index;
    [SerializeField]GameObject checkpointParrent;
    int _totalItems;

    [NonSerialized]public GameObject[] checkpoints;
    #endregion


    #region MonoBehaviour
    private void Awake ()
    {
        player = GameObject.Find("Player").transform;
        _totalItems = checkpointParrent.transform.childCount;
        checkpoints = new GameObject[_totalItems];
        for (int i = 0; i < _totalItems; i++)
        {
            checkpoints[i] = checkpointParrent.transform.GetChild(i).gameObject;

        }
    }
    #endregion

    void OnTriggerEnter2D (Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (index > DataContainer.checkpointIndex)
            {
                Debug.Log(index);
                DataContainer.checkpointIndex = index;


            }

        }
    }

}
