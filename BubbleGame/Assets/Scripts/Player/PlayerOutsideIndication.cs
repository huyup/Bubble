using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// FIXME:矢印のモデルを別の機会に用意する必要があり
/// FIXME:誤差がある
/// </summary>
public class PlayerOutsideIndication : MonoBehaviour
{
    GameObject arrow;
    Camera targetCamera;

    Rect viewPortRect = new Rect(0, 0, 1, 1);

    void Start()
    {
        targetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        arrow=transform.Find("Arrow").gameObject;
    }

    void Update()
    {
        Vector3 viewportPos = targetCamera.WorldToViewportPoint(transform.position);

        if (viewPortRect.Contains(viewportPos))
        {
            Renderer[] renderers = arrow.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = false;
        }
        else
        {
            Renderer[] renderers = arrow.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = true;
        }
        
    }
}
