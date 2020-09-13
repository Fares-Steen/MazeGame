using UnityEngine;

public class SeasonsButtons : MonoBehaviour
{

    private bool clicked = false;
    private bool setIdel = false;

    void Start()
    {

    }


    void Update()
    {

        SetClickedPosition();
        SetIdelPosistion();

    }


    private void SetClickedPosition()
    {
        if (clicked)
        {
            if (transform.localPosition.z < -0.401f)
            {
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            }
            else
            {
                clicked = false;
            }
        }



    }

    private void SetIdelPosistion()
    {
        if (setIdel)
        {
            if (transform.localPosition.z > -0.659f)
            {
                transform.Translate(Vector3.back * 2 * Time.deltaTime);
            }
            else
            {
                setIdel = false;
            }
        }

    }

    public void setButtonClicked()
    {
        clicked = true;
    }

    public void setButtonIdel()
    {
        setIdel = true;
    }
}
