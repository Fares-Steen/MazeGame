using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherMachine : MonoBehaviour
{
    public LayerMask clickableLayer;

    public Texture2D pointerCursor;
    public Texture2D targetCursor;
    private List<Seasons> clickedButtons = new List<Seasons>();
    private bool winterClicked;
    private bool summerClicked;
    private bool fallClicked;
    private bool springClicked;
    private AudioSource machineAudioSource;

    [SerializeField]
    private AudioClip[] machineSounds;


    void Start()
    {
        machineAudioSource = transform.GetComponent<AudioSource>();
    }
    void Update()
    {
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

            if (hit.collider.gameObject.tag == "WinterButton")
            {
                WinterClickEvent(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.tag == "SummerButton")
            {
                SummerClickEvent(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.tag == "FallButton")
            {
                FallClickEvent(hit.collider.gameObject);

            }
            else if (hit.collider.gameObject.tag == "SpringButton")
            {
                SpringClickEvent(hit.collider.gameObject);

            }

            CheckIfAllButtonClicked();
        }
    }

    private void CheckIfAllButtonClicked()
    {
        if (clickedButtons.Count == 4)
        {
            if (CheckIfOrderRight())
            {


                GameObject.FindGameObjectWithTag("DoorWithLight1").GetComponent<DoorWithLight>().SetOpenDoor();
                GameObject.FindObjectOfType<PlanetMachine>().ActivateMachine();
                Destroy(this);
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
        winterClicked = false;
        summerClicked = false;
        fallClicked = false;
        springClicked = false;
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
        if (clickedButtons.IndexOf(Seasons.winter) == 0 && clickedButtons.IndexOf(Seasons.spring) == 1 && clickedButtons.IndexOf(Seasons.summer) == 2 && clickedButtons.IndexOf(Seasons.fall) == 3)
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

    private void WinterClickEvent(GameObject winterButton)
    {
        if (!winterClicked)
        {
            PlaySlidSound();
            winterClicked = true;
            clickedButtons.Add(Seasons.winter);
            winterButton.GetComponent<SeasonsButtons>().setButtonClicked();



        }
    }
    private void SummerClickEvent(GameObject summerButton)
    {
        if (!summerClicked)
        {
            PlaySlidSound();
            summerClicked = true;
            clickedButtons.Add(Seasons.summer);
            summerButton.GetComponent<SeasonsButtons>().setButtonClicked();
        }
    }
    private void SpringClickEvent(GameObject springButton)
    {
        if (!springClicked)
        {
            PlaySlidSound();
            springClicked = true;
            clickedButtons.Add(Seasons.spring);
            springButton.GetComponent<SeasonsButtons>().setButtonClicked();
        }
    }
    private void FallClickEvent(GameObject fallButton)
    {
        if (!fallClicked)
        {
            PlaySlidSound();
            fallClicked = true;
            clickedButtons.Add(Seasons.fall);
            fallButton.GetComponent<SeasonsButtons>().setButtonClicked();


        }
    }


}

public enum Seasons
{
    winter = 1,
    summer,
    fall,
    spring
}
