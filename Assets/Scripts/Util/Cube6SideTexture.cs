using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameUtil
{
	[ExecuteAlways]
	public class Cube6SideTexture : MonoBehaviour
	{
		[Header("Textures")] 
		public Texture2D topTexture;
		public Texture2D bottomTexture;
		public Texture2D leftTexture;
		public Texture2D rightTexture;
		public Texture2D frontTexture;
		public Texture2D backTexture;
		[Header("UVSplitPoints")] 
		public Vector2 topPoint;
		public Vector2 bottomPoint;
		public Vector2 leftPoint;
		public Vector2 rightPoint;
		public Vector2 frontPoint;
		public Vector2 backPoint;

		public enum CubeFaceType
		{
			Top,
			Bottom,
			Left,
			Right,
			Front,
			Back
		};

		private MeshRenderer mesh;

		private Mesh m_mesh;

		private Texture2D m_Texture;

		// Start is called before the first frame update
		 public void Start()
		{
			MeshFilter meshFilter = GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				Debug.LogError("Script needs MeshFilter component");
				return;
			}

#if UNITY_EDITOR
			Mesh meshCopy = Mesh.Instantiate(meshFilter.sharedMesh) as Mesh; // Make a deep copy
			meshCopy.name = "Cube";
			m_mesh = meshFilter.mesh = meshCopy; // Assign the copy to the meshes
#else
		m_mesh = meshFilter.mesh;
#endif
			if (m_mesh == null || m_mesh.uv.Length != 24)
			{
				Debug.LogError("Script needs to be attached to built-in cube");
				return;
			}

			UpdateMeshUVS();
		}
#if UNITY_EDITOR
		public void OnValidate()
		{
			// GenMergedTexture();
			Start();
		}
#endif


		public static Texture2D MergeMoreTex(Texture2D[] texs)
		{
			if (texs.Length < 1) return null;
			Texture2D nTex = new Texture2D(4096, 4096, TextureFormat.ARGB32, true);
			Color[] colors = new Color[nTex.width * nTex.height];
			int startw, starth;
			startw = 0; //横向写入偏移
			starth = 0; //纵向写入偏移
			//以下计算横向跟纵向个数可能会出现大部分空余，这里的计算能满足我自己的需求，没有测试过复杂情况。有需要可以修改
			//根据数量计算横向个数
			int wcnt = Mathf.CeilToInt(Mathf.Sqrt(texs.Length / 1.8f));
			//纵向个数
			int hcnt = Mathf.FloorToInt(wcnt * (1280f / 720f));
			//单张高度
			int oneh = Mathf.FloorToInt(4096 / hcnt);
			//单张宽度
			int onew = Mathf.FloorToInt(4096 / wcnt);
			for (int i = 0; i < texs.Length; i++)
			{
				for (int h = 0; h < oneh; h++)
				{
					for (int w = 0; w < onew; w++)
					{
						Color color = texs[i].GetPixelBilinear((float) w / onew, (float) h / oneh);
						int index = h * nTex.width + w + startw + (starth * 4096);
						if (index >= colors.Length)
						{
							Debug.LogError("数组越界");
							continue;
						}

						if (colors[index] == null)
						{
							colors[index] = color;
							continue;
						}

						colors[index] = color;
					}
				}

				startw += onew;
				if (startw + onew > 4096)
				{
					starth += oneh;
					startw = 0;
				}
			}

			nTex.SetPixels(colors);
			nTex.Apply();
			return nTex;
		}

		void UpdateMeshUVS()
		{
			Vector2[] uvs = m_mesh.uv;
			// Front
			SetFaceTexture(CubeFaceType.Front, uvs);
			// Top
			SetFaceTexture(CubeFaceType.Top, uvs);
			// Back
			SetFaceTexture(CubeFaceType.Back, uvs);
			// Bottom
			SetFaceTexture(CubeFaceType.Bottom, uvs);
			// Left
			SetFaceTexture(CubeFaceType.Left, uvs);
			// Right        
			SetFaceTexture(CubeFaceType.Right, uvs);
			m_mesh.uv = uvs;
		}

		Vector2[] GetUVS(float originX, float originY)
		{
			Vector2[] uvs = new Vector2[4];
			uvs[0] = new Vector2(originX / 2.0f, originY / 3.0f);
			uvs[1] = new Vector2((originX + 1) / 2.0f, originY / 3.0f);
			uvs[2] = new Vector2(originX / 2.0f, (originY + 1) / 3.0f);
			uvs[3] = new Vector2((originX + 1) / 2.0f, (originY + 1) / 3.0f);
			return uvs;
		}

		void SetFaceTexture(CubeFaceType faceType, Vector2[] uvs)
		{
			if (faceType == CubeFaceType.Front)
			{
				Vector2[] newUVS = GetUVS(frontPoint.x, frontPoint.y);
				uvs[0] = newUVS[0];
				uvs[1] = newUVS[1];
				uvs[2] = newUVS[2];
				uvs[3] = newUVS[3];
			}
			else if (faceType == CubeFaceType.Back)
			{
				Vector2[] newUVS = GetUVS(backPoint.x, backPoint.y);
				uvs[10] = newUVS[3];
				uvs[11] = newUVS[2];
				uvs[6] = newUVS[1];
				uvs[7] = newUVS[0];
			}
			else if (faceType == CubeFaceType.Top)
			{
				Vector2[] newUVS = GetUVS(topPoint.x, topPoint.y);
				uvs[8] = newUVS[0];
				uvs[9] = newUVS[1];
				uvs[4] = newUVS[2];
				uvs[5] = newUVS[3];
			}
			else if (faceType == CubeFaceType.Bottom)
			{
				Vector2[] newUVS = GetUVS(bottomPoint.x, bottomPoint.y);
				uvs[12] = newUVS[0];
				uvs[14] = newUVS[3];
				uvs[15] = newUVS[1];
				uvs[13] = newUVS[2];
			}
			else if (faceType == CubeFaceType.Left)
			{
				Vector2[] newUVS = GetUVS(leftPoint.x, leftPoint.y);
				uvs[16] = newUVS[0];
				uvs[18] = newUVS[3];
				uvs[19] = newUVS[1];
				uvs[17] = newUVS[2];
			}
			else if (faceType == CubeFaceType.Right)
			{
				Vector2[] newUVS = GetUVS(rightPoint.x, rightPoint.y);
				uvs[20] = newUVS[0];
				uvs[22] = newUVS[3];
				uvs[23] = newUVS[1];
				uvs[21] = newUVS[2];
			}
		}

		public void GenMergedTexture()
		{
			mesh = GetComponent<MeshRenderer>();
			Texture2D[] texture2Ds = new Texture2D[6];
			texture2Ds[0] = topTexture;
			texture2Ds[1] = bottomTexture;
			texture2Ds[2] = leftTexture;
			texture2Ds[3] = rightTexture;
			texture2Ds[4] = frontTexture;
			texture2Ds[5] = backTexture;
			m_Texture = MergeMoreTex(texture2Ds);

			mesh.sharedMaterial.SetTexture("_MainTex", m_Texture);
			mesh.sharedMaterial.SetTexture("_BaseMap", m_Texture);		
			}

		public void SaveMergedTexture()
		{
			if (m_Texture == null)
			{
				Debug.LogError("请先生成图片");
				return;
			}

			m_Texture.Apply();
			byte[] dataBytes = m_Texture.EncodeToPNG();
			string savePath = Application.dataPath + "/Art/MergedTextures/" + gameObject.name + ".png";
			FileStream fileStream = File.Open(savePath, FileMode.OpenOrCreate);
			fileStream.Write(dataBytes, 0, dataBytes.Length);
			fileStream.Close();
			UnityEditor.AssetDatabase.SaveAssets();
			UnityEditor.AssetDatabase.Refresh();
		}
	}
}