using UnityEngine;

public class FloorToOpenDoor : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OpenTheDoor();
        }
    }

    private static void OpenTheDoor()
    {
        GameObject.FindGameObjectWithTag("DoorWithLight3").GetComponent<DoorWithLight>().SetOpenDoor();
    }
}
