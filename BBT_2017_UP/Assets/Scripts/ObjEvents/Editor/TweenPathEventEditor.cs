using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TransformMoveEvent), true)]
public class TweenPathEventEditor : TweenEventEditor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TransformMoveEvent cast = (TransformMoveEvent)target;
        cast.tweenDuration = EditorGUILayout.FloatField("Duration", cast.tweenDuration);
        cast.targetObj = (Transform)EditorGUILayout.ObjectField("Target", cast.targetObj, typeof(Transform), true);
    }
}
