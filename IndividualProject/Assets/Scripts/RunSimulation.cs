using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunSimulation : MonoBehaviour
{
    // value simplified for simulation and better visualization
    public float speed = 30.0f; // km/hr
    
    public int predictionStepsPerframe = 6;
    public Vector2 bulletVelocity;

    
    // Get important values from previous script
    private float rotation;
    private float drag;
    private float wind;
    
    // used for rotating gun
    private GameObject gun;
    
    // represents the restart button
    private Button reset;

    // used for applying wind to bullet
    private bool starting = true;
    // Value simplified for simulation and better visualisation
    public float windSpeed = 2.0f; // km/hr


    private void resetSimulator()
    {
        // transition to beginning scene
        SceneManager.LoadScene("GunDisplay");
    }

    /**
     * this will be primarly used to set values of sliders to usable values
     */
    private void Awake()
    {
        // Get the values from the static class
        rotation = Values.Rotation;
        drag = Values.Drag;
        wind = Values.Wind;
        
        // is used for restarting the simulation
        reset = GameObject.Find("Reset").GetComponent<Button>();
        reset.onClick.AddListener( resetSimulator );
    }

    // Start is called before the first frame update
    void Start()
    {
        // match gun rotation of prev scene
        gun = GameObject.Find("Gun");
        gun.transform.Rotate(Vector3.back *  ( rotation - 90 )  );
        
    }

    // Update is called once per frame
    void Update()
    {
        if (starting)
        {
            // Apply initial wind force
            speed = speed - (windSpeed * wind);
            if (drag > 0)
            {
                bulletVelocity = this.transform.right * ( speed * drag );
            }
            else
            {
                bulletVelocity = this.transform.right * speed;
            }
            starting = false;
        }
        else
        {
            Vector2 increase;
            Vector2 decrease;
            
            Vector2 point1 = this.transform.position;
            float stepSize = 1.0f / predictionStepsPerframe;
            for (float step = 0; step < 1; step += stepSize)
            {
                // calculate movement of bullet
                increase = Physics2D.gravity * ( stepSize * Time.deltaTime );
                
                // calculate wind resistance
                decrease = Vector2.right * ( windSpeed * wind * stepSize * Time.deltaTime) ;
                
                // Apply bullet drag, this will scale other results
                if (drag > 0)
                {
                    bulletVelocity += (increase - decrease) * drag;
                }
                else
                {
                    bulletVelocity += increase - decrease;
                }
                
                Vector2 point2 = point1 + bulletVelocity * ( stepSize * Time.deltaTime );

                point1 = point2;
            }

            this.transform.position = point1;
        }    
    }

    /**
     * This will be a visualizing function which will draw an estimate
     * of trajectory outcome in scene mode without drag and wind
     */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        // make it so its actually at the barrel
        Vector2 point1 = this.transform.position;
        Vector2 predictedBulletVelocity = bulletVelocity;
        float stepSize = 0.01f;
        // iterate bullet simulation in every frame
        for( float step = 0; step < 100; step += 1 )
        {
            predictedBulletVelocity += Physics2D.gravity * stepSize;
            // Apply wind resistance to predicted bullet Velocity
            predictedBulletVelocity -= Vector2.right * ( windSpeed * wind * stepSize ) ;
            Vector2 point2 = point1 + predictedBulletVelocity * 0.01f;
            Gizmos.DrawLine( point1, point2 );
            point1 = point2;
        }
    }
}
