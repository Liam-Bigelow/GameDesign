using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetValues : MonoBehaviour
{
    
    // these are just the slider values
    protected float windResistance;
    protected float bulletDrag;
    
    // Gun rotation will be between 0 - 360 starting at 90
    protected float gunRotation;
    // is needed to rotate gun on screen
    private Slider gundeg;
    private float degree;
    private float prevDegree;
    private GameObject gun;
    
    // This will be used to transition when the Run button is clicked
    private Button run;

    private void Awake()
    {
        // Is used for rotation of gun on screen
        gundeg = GameObject.Find("GunRotation").GetComponent<Slider>();
        gundeg.onValueChanged.AddListener( rotateGun );
        prevDegree = gundeg.value;
        gun = GameObject.Find("Gun");
        
        // is used for running the simulation
        run = GameObject.Find("Run").GetComponent<Button>();
        run.onClick.AddListener( RunSim );
        
        setSliderInfo();
        getSliderInfo();
    }

    void getSliderInfo()
    {
        windResistance = GameObject.Find("WindSpeed").GetComponent<Slider>().value;
        bulletDrag = GameObject.Find("BulletDrag").GetComponent<Slider>().value;
        gunRotation = GameObject.Find("GunRotation").GetComponent<Slider>().value;
    }

    /**
     * this will be used to set the sliders to they're previously set position
     * when the restart is selected
     */
    void setSliderInfo()
    {
        if ( Values.ValuesChanged )
        {
            GameObject.Find("WindSpeed").GetComponent<Slider>().value = Values.Wind;
            GameObject.Find("BulletDrag").GetComponent<Slider>().value = Math.Abs( Values.Drag -1);
            GameObject.Find("GunRotation").GetComponent<Slider>().value = Values.Rotation / 360;
        }
    }

    void rotateGun( float deg )
    {
        // change in cur and prev
        float delta = deg - prevDegree;
        gun.transform.Rotate(Vector3.back * ( delta * 360 ) );
 
        // Set our previous value for the next change
        prevDegree = deg;
    }

    void RunSim()
    {
        // this sets a static script so we can remeber values between scenes
        Values.Drag = Math.Abs( bulletDrag -1);
        Values.Rotation = gunRotation * 360;
        Values.Wind = windResistance;
        Values.ValuesChanged = true;
        
        // transition to the new scene
        SceneManager.LoadScene("GunSimulation");
    }

    // Update is called once per frame
    void Update()
    {
        getSliderInfo();
    }
}
