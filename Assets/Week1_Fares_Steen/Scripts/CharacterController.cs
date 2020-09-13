using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float speed = 5.0f;




    void FixedUpdate()
    {
        MovePlayer();

    }

    private void MovePlayer()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);
    }



}
