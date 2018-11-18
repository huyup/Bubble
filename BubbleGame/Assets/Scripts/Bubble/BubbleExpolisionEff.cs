using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleExpolisionEff : MonoBehaviour {

    ParticleSystem bubbleExplosionEff;

    float minStartSize;
    float maxStartSize;

    float sphereRadius;
    [SerializeField]
    float fatorToCalTheMinSize = 0.5f;

    [SerializeField]
    float fatorToCalTheMaxSize = 1.3f;

    [SerializeField]
    float fatorToCalTheRadius = 2f;

    // Use this for initialization
    void Start () {
        bubbleExplosionEff = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
    }
    public void SaveEffStartProperty(float _bubbleSize)
    {
        minStartSize = _bubbleSize * fatorToCalTheMinSize;
        maxStartSize = _bubbleSize * fatorToCalTheMaxSize;

        sphereRadius = _bubbleSize * fatorToCalTheRadius;
    }
    public void ChangeEffStartProperty()
    {
        ParticleSystem.MainModule mainModule = bubbleExplosionEff.main;
        
        mainModule.startSize = new ParticleSystem.MinMaxCurve(minStartSize, maxStartSize);

        ParticleSystem.ShapeModule shapeModule = bubbleExplosionEff.shape;

        shapeModule.radius = sphereRadius;
    }
}
