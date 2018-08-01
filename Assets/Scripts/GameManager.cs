using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            MovingCube.CurrentCube.Stop();
        }
    }
}
