using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMachine : MonoBehaviour
{
    public LayerMask clickableLayer;

    public Texture2D pointerCursor;
    public Texture2D targetCursor;
    private List<Planets> clickedButtons = new List<Planets>();
    private bool earthClicked;
    private bool marsClicked;
    private bool satrumClicked;
    private bool jupiterClicked;
    private bool machineIsActive;
    private AudioSource machineAudioSource;

    [SerializeField]
    private AudioClip[] machineSounds;
    private void Start()
    {
        machineAudioSource = transform.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (machineIsActive)
            changeCursor();
    }

    void PlaySlidSound()
    {
        machineAudioSource.clip = machineSounds[1];
        machineAudioSource.Play();
    }

    void PlayWrongAnswerSound()
    {
        machineAudioSource.clip = machineSounds[0];
        machineAudioSource.Play();
    }
    private void changeCursor()
    {
        RaycastHit hit;
        if (CheckIfMouseOnClickablLayer(out hit))
        {


            ChangeCursorToTarget();

            HandleMouseClicks(hit);
        }
        else
        {
            ChangeCursorToPointer();
        }
    }

    private void HandleMouseClicks(RaycastHit hit)
    {
        if (Input.GetMouseButtonDown(0))//mouse left clicked
        {

            if (hit.collider.gameObject.tag == "EarthButton")
            {
                EarthClickEvent(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.tag == "MarsButton")
            {
                MarsClickEvent(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.tag == "SatrumButton")
            {
                SatrumClickEvent(hit.collider.gameObject);

            }
            else if (hit.collider.gameObject.tag == "JupiterButton")
            {
                JupiterClickEvent(hit.collider.gameObject);

            }

            CheckIfAllButtonsClicked();
        }
    }

    private void CheckIfAllButtonsClicked()
    {
        if (clickedButtons.Count == 4)
        {
            if (CheckIfOrderRight())
            {


                GameObject.FindGameObjectWithTag("DoorWithLight2").GetComponent<DoorWithLight>().SetOpenDoor();
            }
            else
            {

                StartCoroutine(ResetButtons());

            }
        }
    }

    private IEnumerator ResetButtons()
    {
        PlayWrongAnswerSound();
        earthClicked = false;
        marsClicked = false;
        satrumClicked = false;
        jupiterClicked = false;
        clickedButtons.Clear();
        yield return new WaitForSeconds(1);
        var allButtons = GameObject.FindObjectsOfType<SeasonsButtons>();
        foreach (var button in allButtons)
        {
            button.setButtonIdel();
        }

    }
    private bool CheckIfOrderRight()
    {
        bool rightOrder = false;
        if (clickedButtons.IndexOf(Planets.earth) == 0 && clickedButtons.IndexOf(Planets.mars) == 1 && clickedButtons.IndexOf(Planets.satrum) == 2 && clickedButtons.IndexOf(Planets.jupiter) == 3)
        {
            rightOrder = true;
        }

        return rightOrder;
    }

    private bool CheckIfMouseOnClickablLayer(out RaycastHit hit)
    {
        var isClickable = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value);
        return isClickable;
    }


    private void ChangeCursorToTarget()
    {
        Cursor.SetCursor(targetCursor, new Vector2(16, 16), CursorMode.Auto);
    }

    private void ChangeCursorToPointer()
    {
        Cursor.SetCursor(pointerCursor, new Vector2(16, 16), CursorMode.Auto);
    }

    private void EarthClickEvent(GameObject earthButton)
    {
        if (!earthClicked)
        {
            PlaySlidSound();
            earthClicked = true;
            clickedButtons.Add(Planets.earth);
            earthButton.GetComponent<SeasonsButtons>().setButtonClicked();



        }
    }
    private void MarsClickEvent(GameObject marsButton)
    {
        if (!marsClicked)
        {
            PlaySlidSound();

            marsClicked = true;
            clickedButtons.Add(Planets.mars);
            marsButton.GetComponent<SeasonsButtons>().setButtonClicked();
        }
    }
    private void JupiterClickEvent(GameObject jupiterButton)
    {
        if (!jupiterClicked)
        {
            PlaySlidSound();

            jupiterClicked = true;
            clickedButtons.Add(Planets.jupiter);
            jupiterButton.GetComponent<SeasonsButtons>().setButtonClicked();
        }
    }
    private void SatrumClickEvent(GameObject satrumButton)
    {
        if (!satrumClicked)
        {
            PlaySlidSound();

            satrumClicked = true;
            clickedButtons.Add(Planets.satrum);
            satrumButton.GetComponent<SeasonsButtons>().setButtonClicked();


        }
    }

    public void ActivateMachine()
    {
        machineIsActive = true;
    }


}

public enum Planets
{
    earth = 1,
    mars,
    jupiter,
    satrum
}
