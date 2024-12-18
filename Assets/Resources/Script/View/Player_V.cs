using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_V : MonoBehaviour
{

    [SerializeField]public Player player ;

    public Camera camera;
    public Vector3 original;
    private Vector3 originalPosition;
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0.1f;
    public float shakeFrequency = 0.1f;
    public bool HitDamage;

    public bool addscore;
    public bool divscore;
    private int valuescore;
    [Header("GUI")]
    [SerializeField] Text score;
    void Start()
    {
        player = new Player(player.health);
        camera = Camera.main;
        original = camera.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            camera.transform.localPosition = originalPosition + new Vector3(x, y, 0);

            shakeDuration -= Time.deltaTime * shakeFrequency;
        }
        else
        {
            shakeDuration = 0f;
            camera.transform.localPosition = originalPosition;
        }

        if (addscore)
        {
            valuescore += 20;
            score.text = string.Format("Score:{0}",valuescore  );
            addscore = false;
            return;
        }
        if (divscore) { 
            valuescore -= 20;
            score.text = string.Format("Score:{0}", valuescore);
            divscore = false;
            return;
        }
        Debug.Log(valuescore);
        
    }
}
