using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //array of all the backgrounds to be parallaxed
    public Transform[] backgrounds;

    //proportion of cameras movement to move the backgrounds by
    private float[] parallaxScales;

    //Parallax smooth, gt 0
    public float smoothing = 1f;

    //camera
    private Transform cam;

    //position of camera in the previous frame;
    private Vector3 preCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        preCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*(-1);
        }
    }

    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (preCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing*Time.deltaTime);
        }

        //set the precampos to the camera position at the end of the frame
        preCamPos = cam.position;
    }

}
