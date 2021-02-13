using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region Public members
[Reorderable("Local Transformation")] // permet d'avoir un tableau réorganisable
public BezierTransformation[] localTransformations = new BezierTransformation[1];
#endregion

#region public functions
public class transformeBezierCurve : MonoBehaviour
{
    public override Vector3 GetPosition(float ratio)
    {
        Matrix4x4 splineMatrix = base.GetMatrix(ratio);
        Matrix4x4 finalMatrix = splineMatrix;

        for (int i = 0; i < localTransformations.Length; ++i)
        {
            int index = (localTransformations.Length - 1) - i; // applique les tranformation en sens inverse
            Matrix4x4 localTransformationMatrix = localTransformations[index].GetMatrix();
            finalMatrix = finalMatrix * localTransformationMatrix;
        }

        Vector3 origin = Vector3.zero; // the center of our matrix is the local position 0,0,0
        return finalMatrix.MultiplyPoint(origin);
    }
}
