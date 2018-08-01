using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        //first cube that spawns automatically becomes the last cube
        if (LastCube == null)
            //remember if you rename 'start' cube object in unity the whole game breaks
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();

        CurrentCube = this;
    }

    // chop and stop the cube
    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = transform.position.z - LastCube.transform.position.z;

        SplitCubeOnZ(hangover);
    }

    private void SplitCubeOnZ(float hangover)
    {
        // newZSize is based on the original cube minus the hangover value from stop() method
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);

        // new size minus the old size
        float fallingBlockSize = transform.localScale.z - newZSize;

        // now figure out new z positions after cube is cut
        float newZPosition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f;

        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(transform.position.x, transform.position.y, cubeEdge);
        sphere.transform.localScale = Vector3.one * 0.1f;

        // spawn the dropped cube
        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);

    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // we need the scale and position
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockZPosition);

    }

    // Update is called once per frame
    private void Update()
    {
        // speed is float
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
