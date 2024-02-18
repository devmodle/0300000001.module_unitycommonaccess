using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 메쉬 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 메쉬를 생성한다 */
	public static Mesh MakeMesh(string a_oName, 
		List<Vector3> a_oVertexList, List<int> a_oIdxList, List<Vector3> a_oNormalList, List<Vector2> a_oUVList, MeshTopology a_eTopology = MeshTopology.Triangles) {

		var oMesh = new Mesh();
		oMesh.name = a_oName;

		oMesh.SetVertices(a_oVertexList);
		oMesh.SetIndices(a_oIdxList, a_eTopology, KCDefine.B_VAL_0_INT);
		oMesh.SetNormals(a_oNormalList);
		oMesh.SetUVs(KCDefine.B_VAL_0_INT, a_oUVList);

		return oMesh;
	}
	#endregion // 클래스 함수
}
