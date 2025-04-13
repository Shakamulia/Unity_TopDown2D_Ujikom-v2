using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class BossAreaZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float targetOrthoSize = 10f;
    public float zoomDuration = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Masuk Trigger: " + collision.name); // DEBUG!

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Terdeteksi!"); // DEBUG!
            DOTween.To(() => virtualCamera.m_Lens.OrthographicSize,
                       x => virtualCamera.m_Lens.OrthographicSize = x,
                       targetOrthoSize, zoomDuration);
        }
    }

}
