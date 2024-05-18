using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] GameObject[] textObjects;
    [SerializeField] Camera cam;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] Transform player;
    [SerializeField] float threshold; //La cantidad de desplazamiento
    [SerializeField] float mapThreshold; //Desplazamiento en modo mapa
    float initialThreshold;
    float initialOrtho;
    
    float t;
    bool mapEnabled;

    [SerializeField] VolumeProfile volumePost;

    

    private void Start()
    {
        initialThreshold = threshold;
        mapEnabled = false;
        initialOrtho = 12f;
        if (vcam.m_Lens.OrthographicSize != initialOrtho) { vcam.m_Lens.OrthographicSize = initialOrtho; }
        if (volumePost.TryGet(out FilmGrain filmGrainMap)) { filmGrainMap.intensity.value = 0; }
        if (volumePost.TryGet(out ColorAdjustments colorAdjustments)) { colorAdjustments.saturation.value = 0f; colorAdjustments.colorFilter.value = new Color(1, 1, 1, 1); }
        if (volumePost.TryGet(out LiftGammaGain gainMap)) { gainMap.gamma.value=new Vector4(1,1,1,0); }
        if (volumePost.TryGet(out PaniniProjection paniniProjection)) { paniniProjection.distance.value = 0; }
        if (volumePost.TryGet(out ChromaticAberration chromaticAberration)) { chromaticAberration.intensity.value = 0; }
        for (int i = 0; i <= textObjects.Length-1; i++)
        {
            textObjects[i].SetActive(false);
        }
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //Checkea pos mouse
        Vector3 targetPos = new Vector3((player.position.x + mousePos.x) , (player.position.y + mousePos.y))/2f; //Calcula la distancia entre player y mousepos

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x); //Constraint (Value, lower threshold, upper threshold
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPos; //La pos de la camara = la pos del mouse ??

        if (Input.GetKeyDown(KeyCode.Space) && !mapEnabled)
        {
            ToMap();   
        }
        else if (Input.GetKeyDown(KeyCode.Space) && mapEnabled)
        {
            FromMap();
        }

        t = Time.time * .1f; ;
    }

    void ToMap()
    {
        vcam.m_Lens.OrthographicSize = mapThreshold;
        mapEnabled = true;
        threshold = mapThreshold;
        if (volumePost.TryGet(out FilmGrain filmGrainMap)) { filmGrainMap.intensity.value = .6f; }
        if (volumePost.TryGet(out ColorAdjustments colorAdjustments)) { colorAdjustments.saturation.value = -80f; colorAdjustments.colorFilter.value = new Color(.4f, .7f, .8f, 1); }
        if (volumePost.TryGet(out LiftGammaGain gainMap)) { gainMap.gamma.value = new Vector4(.5f, .7f, .8f, 1); }
        if (volumePost.TryGet(out PaniniProjection paniniProjection)) { paniniProjection.distance.value = 1; }
        if (volumePost.TryGet(out ChromaticAberration chromaticAberration)) { chromaticAberration.intensity.value = .5f; }

        for (int i = 0; i <= textObjects.Length-1; i++)
        {
            textObjects[i].SetActive(true);
        }
    }
    void FromMap()
    {
        vcam.m_Lens.OrthographicSize = initialOrtho;
        mapEnabled = false;
        threshold = initialThreshold;
        if (volumePost.TryGet(out FilmGrain filmGrainMap)) { filmGrainMap.intensity.value = 0; }
        if (volumePost.TryGet(out ColorAdjustments colorAdjustments)) { colorAdjustments.saturation.value = 0f; colorAdjustments.colorFilter.value = new Color(1, 1, 1, 1); }
        if (volumePost.TryGet(out LiftGammaGain gainMap)) { gainMap.gamma.value = new Vector4(1, 1, 1, 0); }
        if (volumePost.TryGet(out PaniniProjection paniniProjection)) { paniniProjection.distance.value = 0; }
        if (volumePost.TryGet(out ChromaticAberration chromaticAberration)) { chromaticAberration.intensity.value = 0; }

        for (int i = 0; i <= textObjects.Length-1; i++)
        {
            textObjects[i].SetActive(false);
        }
    }
}
