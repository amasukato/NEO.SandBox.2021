using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;


    private Vector3 currentCamPos;
    private Vector3 currentCamRot;

    private Vector3 previousCamPos;
    private Vector3 previousCamRot;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    void Start()
    {
        PlayerTransform = GameObject.Find("Player").transform;
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }

}
