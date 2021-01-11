using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BoomShroom : MonoBehaviour
{
    private GameObject MainLight { get; set; }
    private Light2D[] BulbLights { get; set; }
    private ParticleSystem IdleSpores { get; set; }

    [SerializeField] private ParticleSystem explosionSpores;
    [SerializeField] bool isActive;
    [SerializeField] float minimumIntensity;
    [SerializeField] float maximumIntensity;
    [SerializeField] float explosionIntensity;
    [SerializeField] float lightAdjustmentSpeed;
    [SerializeField] bool hasExploded;
    [SerializeField] float timer;
    [SerializeField] float inactiveTime;

    public enum LightState
    {
        DIM,
        BRIGHTEN,
        EXPLODE
    }
    [SerializeField] private LightState lightState;

    // Start is called before the first frame update
    void Start()
    {
        MainLight = transform.GetChild(0).gameObject;
        BulbLights = new Light2D[9];
        BulbLights[0] = MainLight.transform.GetChild(0).GetComponent<Light2D>();
        BulbLights[1] = MainLight.transform.GetChild(1).GetComponent<Light2D>();
        BulbLights[2] = MainLight.transform.GetChild(2).GetComponent<Light2D>();
        BulbLights[3] = MainLight.transform.GetChild(3).GetComponent<Light2D>();
        BulbLights[4] = MainLight.transform.GetChild(4).GetComponent<Light2D>();
        BulbLights[5] = MainLight.transform.GetChild(5).GetComponent<Light2D>();
        BulbLights[6] = MainLight.transform.GetChild(6).GetComponent<Light2D>();
        BulbLights[7] = MainLight.transform.GetChild(7).GetComponent<Light2D>();
        BulbLights[8] = MainLight.transform.GetChild(8).GetComponent<Light2D>();
        lightState = LightState.DIM;

        IdleSpores = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleBoomShroom();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BoomerangObj>() != null)
        {
            BoomerangObj boomerang = collision.gameObject.GetComponent<BoomerangObj>();

            if(boomerang.CurrentBoomerang.Type == Boomerang.BoomerangTypes.FIRE)
            {
                // Trigger the boom shroom
                SetLightState(LightState.EXPLODE);
            }
            else
            {
                Debug.Log("That's the wrong boomerang type. Try using the FIRE boomerang!");
            }
        }
    }

    private void HandleBoomShroom()
    {
        if (isActive)
        {
            PulsateLights();
        }
        else
        {
            if(timer < inactiveTime)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                ResetBoomShroom();
            }
        }
    }

    private void SetActiveStatus(bool value)
    {
        if(isActive != value)
        {
            isActive = value;
        }
    }

    private void PulsateLights()
    {
        foreach (Light2D light in BulbLights)
        {
            switch (lightState)
            {
                case LightState.DIM:
                    if (light.intensity >= minimumIntensity)
                    {
                        light.intensity -= lightAdjustmentSpeed * Time.fixedDeltaTime;
                    }
                    else
                    {
                        SetLightState(LightState.BRIGHTEN);
                    }
                    break;

                case LightState.BRIGHTEN:
                    if (light.intensity <= maximumIntensity)
                    {
                        light.intensity += lightAdjustmentSpeed * Time.fixedDeltaTime;
                    }
                    else
                    {
                        SetLightState(LightState.DIM);
                    }
                    break;

                case LightState.EXPLODE:
                    if (light.intensity <= explosionIntensity)
                    {
                        light.intensity += (lightAdjustmentSpeed * 2) * Time.fixedDeltaTime;
                    }
                    else
                    {
                        Explode();
                    }
                    break;
            }
        }
    }

    private void SetLightState(LightState state)
    {
        if(lightState != state)
        {
            lightState = state;
        }
    }

    private void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;

            // Turn off Idle Spores
            if (IdleSpores.isPlaying)
            {
                IdleSpores.Stop();
            }

            // Turn off all lights
            foreach(Light2D light in BulbLights)
            {
                if (light.enabled)
                {
                    light.enabled = false;
                }
            }

            // Trigger explosion particles
            Debug.Log("BOOOOOOOM!");
            SetActiveStatus(false);
        }
    }

    private void ResetBoomShroom()
    {
        hasExploded = false;
        foreach (Light2D light in BulbLights)
        {
            if (!light.enabled)
            {
                light.enabled = true;
                light.intensity = minimumIntensity;
            }
        }
        SetLightState(LightState.BRIGHTEN);
        SetActiveStatus(true);
        timer = 0;
        if (IdleSpores.isStopped)
        {
            IdleSpores.Play();
        }
    }
}
