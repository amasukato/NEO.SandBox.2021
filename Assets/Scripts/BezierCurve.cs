using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class BezierCurve : MonoBehaviour
{

    #region
    public Transform startingPoint;
    public Transform startingTangent;
    public Transform endingPoint;
    public Transform endingTangent;
    #endregion

    #region Unity functions
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if(AreReferencesFilled)
        {
               Handheld.DrawBezier()
        }
    }
    private Vector3 GetPosition_INTERNAL(float ratio)
    {
        ratio = Mathf.Clamp01(ratio); // s'assure que le ratio ne dépasse pas 0 et 1
    
        Vector3 1erpBetweenStartingPoint = Vector3.Lerp(startingPoint.position, startingTangent.position, ratio);
        Vector3 1erpBetweenTangents = Vector3.Lerp(startingTangent.position, endingTangent.position, ratio);
        Vector3 1erpBetweenEndingPoint = Vector3.Lerp(endingTangent.position, endingPoint.position, ratio);

        Vector3 entryCurve = Vector3.Lerp(1erpBetweenStartingPoint, 1erpBetweenTangents, ratio);
        Vector3 exitCurve = Vector3.lerp(1erp)BetweenTangents, 1erpBetweenEndingPoint, ratio);

        Vector3 interpolationCurve = Vector3.lepr(entryCurve, exitCurve, ratio);

        return interpolatedCurve;



    
    }
     
}
