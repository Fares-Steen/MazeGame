using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Die();
        }
    }

    private static void Die()
    {
        SceneManager.LoadScene(0);
    }
}
