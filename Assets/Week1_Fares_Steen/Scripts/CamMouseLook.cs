using UnityEngine;

public class CamMouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    private float sensitivity = 5.0f;
    private float smoothing = 2.0f;

    GameObject charecter;

    void Start()
    {
        charecter = this.transform.parent.gameObject;
    }


    void Update()
    {
        CameraRotate();
    }

    private void CameraRotate()
    {
        var md = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxisRaw("Mouse Y"));


        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        var nextHorizon = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        if (nextHorizon.x > -0.5 && nextHorizon.x < 0.5)
            transform.localRotation = nextHorizon;




        charecter.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, charecter.transform.up);
    }
}
