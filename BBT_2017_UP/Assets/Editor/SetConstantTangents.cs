using UnityEngine;
using UnityEditor;
using System.Collections;

public static class SetConstantTangents {

	[MenuItem ("Tools/Set Constant Tangents")]
	static public void SetConstantTangentsOnAnimationClip() {
		Object[] clips = Selection.GetFiltered( typeof( AnimationClip ), SelectionMode.TopLevel );

		for( int i = 0; i < clips.Length; ++i ) {
			AnimationClip clip = clips[ i ] as AnimationClip;
			EditorCurveBinding[] floatBindings = AnimationUtility.GetCurveBindings( clip );

			for( int j = 0; j < floatBindings.Length; ++j ) {
				EditorCurveBinding binding = floatBindings[ j ];
				AnimationCurve curve = AnimationUtility.GetEditorCurve( clip, binding );

				for( int k = 0; k < curve.keys.Length; ++k ) {
					AnimationUtility.SetKeyLeftTangentMode( curve, k, AnimationUtility.TangentMode.Constant );
					AnimationUtility.SetKeyRightTangentMode( curve, k, AnimationUtility.TangentMode.Constant );
				}

				Undo.RecordObject( clip, "Set Constant Tangents" );
				clip.SetCurve( binding.path, typeof( Transform ), binding.propertyName, curve );
				// AnimationUtility.SetEditorCurve( clip, binding, curve );
			}
		}
	}

}
