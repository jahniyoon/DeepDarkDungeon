using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DungeonExit : MonoBehaviour
{
    public GameObject door;
    public GameObject exit;
    public GameObject keyUI;   

    public float speed;
    private Transform doorTransform;
    bool doorOpen = false;



    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.keyUI == null)
        {
            GameManager.instance.keyUI = keyUI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isDoorOpen && !doorOpen)
        {
            Vector3 newPosition = door.transform.position + Vector3.down * speed * Time.deltaTime;
            newPosition.y = Mathf.Max(newPosition.y, -10.0f);
            door.transform.position = newPosition;
            Invoke("DoorOpen",2f);
            exit.gameObject.SetActive(true);

        }
    }

    public void DoorOpen()
    {
        doorOpen = true;
        Destroy(door);

    }

}
