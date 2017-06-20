using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour {
    public ParticleSystem particleRed;
    public ParticleSystem particleBlue;
    public float smoothness = -.05f;
    public static Quaternion lastWind;
    float dir = 1f;
    Transform target;
    public AnimationCurve windPower;
    public static float WINDPOWER;

    public static Vector2 wind;
    ParticleSystem.VelocityOverLifetimeModule velModRed;
    AudioSource source;
    float maxPower = 0.1f;

	void Start () {
        target = transform.GetChild(0);
        velModRed = particleRed.velocityOverLifetime;
        source = GetComponent<AudioSource>();
        GetMaxPower();

	}

    void GetMaxPower(){
        foreach(Keyframe k in windPower.keys){
            if (k.value > maxPower)
                maxPower = k.value;
        }
    }

	void Update () {
        lastWind = transform.rotation;
        dir = Mathf.Sin(Time.time / 20f);
        transform.Rotate(Vector3.up * Random.Range(-30f * dir, 10f * dir));
        transform.rotation = Quaternion.Lerp(transform.rotation, lastWind, smoothness);
        WINDPOWER = windPower.Evaluate(Time.time);
        source.volume = WINDPOWER / maxPower;
        target.transform.localPosition = Vector3.right * WINDPOWER;
		wind = new Vector2(target.position.x, target.position.z);

        velModRed.xMultiplier = wind.x;
        velModRed.zMultiplier = wind.y;

	}


}
