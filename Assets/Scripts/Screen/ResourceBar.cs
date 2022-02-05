using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResourceBar : MonoBehaviour 
{
    private static float maxResources = 100;
    private static float currentResources;
    private static float regenerationSpeed;
    private static float windScale;

    public Slider resourceBar;

    public static ResourceBar instance;

    private void Awake() 
    {
        instance = this;
    }
    void Start()
    {

        currentResources = maxResources;
        resourceBar.maxValue = maxResources;
        resourceBar.value = maxResources;
        regenerationSpeed = 1f;

        windScale = 0.7f;
        
        // run void regeneration() every 1s after 1s delay
        InvokeRepeating("regeneration", 1f, 1f);
    }

    void regeneration()
    {
        currentResources += regenerationSpeed;
        currentResources = Math.Min(currentResources, maxResources);
        resourceBar.value = currentResources;
    }


    // reduce resource bar when collision happens
    public void collision(int reduction)
    {
        currentResources -= reduction;
        currentResources = Math.Max(currentResources, 0);
    }

    // increase resource bar when items are used
    void addResource(int addition)
    {
        currentResources += addition;
        currentResources = Math.Min(currentResources, maxResources);
    }

    // reduce resource bar when collision happens
    public void windResourceUsage(float windLength)
    {
        float amount = windLength * windScale;
        currentResources -= amount;
        currentResources = Math.Max(currentResources, 0);
        resourceBar.value = currentResources;
    }

    public float getCurrentResources()
    {
        return currentResources;
    }

    public float getWindScale()
    {
        return windScale;
    }

    // sets resource bar capacity
    void setCapacity(int capacity)
    {
        maxResources = capacity;
    }

    // increase resource bar capacity
    void increaseCapacity(int increase)
    {
        maxResources += increase;
    }
    
    // decrease resource bar capacity
    void decreaseCapacity(int decrease)
    {
        maxResources -= decrease;
    }

    // set resource bar regeneration speed
    void setRegenerationSpeed(int regeneration)
    {
        regenerationSpeed = regeneration;
    }

    // increase resource bar regeneration speed
    void increaseRegenerationSpeed(int increase)
    {
        regenerationSpeed += increase;
    }

    // decrease resource bar regeneration speed
    void decreaseRegenerationSpeed(int decrease)
    {
        regenerationSpeed -= decrease;
    }
}