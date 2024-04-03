using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CinematicController : MonoBehaviour
{
    [SerializeField] float wait_time;
    void Start()
    {
        StartCoroutine(Wait_for_end());
    }

    IEnumerator Wait_for_end()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene("mainMenu");
    }
}
