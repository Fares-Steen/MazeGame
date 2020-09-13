using System;
using UnityEngine;

public class DoorWithLight : MonoBehaviour
{
    private bool openDoor = false;
    private bool soundPlayed = false;
    private Transform door;
    private float doorSpeed = 0.4f;
    private float doorTopUp = -6.45f;

    [SerializeField]
    private AudioClip openDoorSound;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        try
        {
            door = transform.Find("VerticalDoor");
            m_AudioSource = door.GetComponent<AudioSource>();
        }
        catch (Exception)
        {


        }
    }


    void Update()
    {
        OpenTheDoor();
    }

    private void OpenTheDoor()
    {
        if (openDoor)
        {
            changeLightColorToGreen();
            MoveTheDoorUp();

        }
    }

    private void MoveTheDoorUp()
    {
        if (door.transform.localPosition.y < doorTopUp)
        {
            door.transform.Translate(Vector3.up * doorSpeed * Time.deltaTime);
            PlayOpenDoorSound();

        }
        else
        {
            openDoor = false;
        }
    }

    public void PlayOpenDoorSound()
    {
        m_AudioSource.clip = openDoorSound;
        if (!soundPlayed)
        {
            soundPlayed = true;
            m_AudioSource.Play();

        }
    }

    public void SetOpenDoor()
    {
        openDoor = true;
    }

    void changeLightColorToGreen()
    {
        GetComponentInChildren<Light>().color = Color.green;
    }
}
