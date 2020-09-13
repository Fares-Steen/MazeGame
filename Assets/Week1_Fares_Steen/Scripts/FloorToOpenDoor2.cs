using UnityEngine;

public class FloorToOpenDoor2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            OpenTheDoor();
        }
    }

    private static void OpenTheDoor()
    {
        GameObject.FindGameObjectWithTag("DoorWithLight4").GetComponent<DoorWithLight>().SetOpenDoor();
    }
}
