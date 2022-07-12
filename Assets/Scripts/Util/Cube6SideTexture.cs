using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameUtil
{
	[Serializable]
	public struct TextureData
	{
		public Texture2D texture;
		public bool reversHorizontal;
		public bool reversVertical;
	}

	[ExecuteAlways]
	public class Cube6SideTexture : MonoBehaviour
	{
		[Header("Textures")] public TextureData topTexture;
		public TextureData bottomTexture;
		public TextureData leftTexture;
		public TextureData rightTexture;
		public TextureData frontTexture;
		public TextureData backTexture;
		[Header("UVSplitPoints")] public Vector2 topPoint;
		public Vector2 bottomPoint;
		public Vector2 leftPoint;
		public Vector2 rightPoint;
		public Vector2 frontPoint;
		public Vector2 backPoint;


		private static int[] front = new[] {0, 1, 2, 3};
		private static int[] back = new[] {7, 6, 11, 10};
		private static int[] top = new[] {8, 9, 4, 5};
		private static int[] bottom = new[] {12, 15, 13, 14};
		private static int[] left = new[] {16, 19, 17, 18};
		private static int[] right = new[] {20, 23, 21, 22};


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

		void ReversUV(Vector2[] origin,int[] indexer, Vector2[] uvs, bool reversHorizon, bool reversVertic)
		{
			if (reversHorizon && reversVertic)
			{
				for (int i = 0; i < 4; i++)
				{
					origin[indexer[i]] = uvs[3-i];
				}
			}
			else if (reversHorizon)
			{
				int sign = 1;
				for (int i = 0; i < 4; i++)
				{
					origin[indexer[i]] = uvs[i + sign];
					sign *= -1;
				}
			}
			else if (reversVertic)
			{
				for (int i = 0; i < 4; i++)
				{
					var index = i - 2;
					if (index < 0)
						index += 4;
					origin[indexer[i]] = uvs[index];
				}
			}
		}
		void SetFaceTexture(CubeFaceType faceType, Vector2[] uvs)
		{
			if (faceType == CubeFaceType.Front)
			{
				Vector2[] newUVS = GetUVS(frontPoint.x, frontPoint.y);

				for (int i = 0; i < 4; i++)
				{
					uvs[front[i]] = newUVS[i];
				}
				if (!frontTexture.reversHorizontal && !frontTexture.reversVertical)
				{
					for (int i = 0; i < 4; i++)
					{
						uvs[front[i]] = newUVS[i];
					}
				}
				else if (frontTexture.reversHorizontal && frontTexture.reversVertical)
				{
					ReversUV(uvs,front,newUVS,true,true);
				}
				else if (frontTexture.reversHorizontal)
				{
					ReversUV(uvs,front,newUVS,true,false);
				}
				else
				{
					ReversUV(uvs,front,newUVS,false,true);
				}
				
			}
			else if (faceType == CubeFaceType.Back)
			{
				Vector2[] newUVS = GetUVS(backPoint.x, backPoint.y);
				for (int i = 0; i < 4; i++)
				{
					uvs[back[i]] = newUVS[i];
				}
				if (!backTexture.reversHorizontal && !backTexture.reversVertical)
				{
					for (int i = 0; i < 4; i++)
					{
						uvs[back[i]] = newUVS[i];
					}
				}
				else if (backTexture.reversHorizontal && backTexture.reversVertical)
				{
					ReversUV(uvs,back,newUVS,true,true);
				}
				else if (backTexture.reversHorizontal)
				{
					ReversUV(uvs,back,newUVS,true,false);
				}
				else
				{
					ReversUV(uvs,back,newUVS,false,true);
				}

			}
			else if (faceType == CubeFaceType.Top)
			{
				Vector2[] newUVS = GetUVS(topPoint.x, topPoint.y);
				for (int i = 0; i < 4; i++)
				{
					uvs[top[i]] = newUVS[i];
				}
				if (!topTexture.reversHorizontal && !topTexture.reversVertical)
				{
					for (int i = 0; i < 4; i++)
					{
						uvs[top[i]] = newUVS[i];
					}
				}
				else if (topTexture.reversHorizontal && topTexture.reversVertical)
				{
					ReversUV(uvs,top,newUVS,true,true);
				}
				else if (topTexture.reversHorizontal)
				{
					ReversUV(uvs,top,newUVS,true,false);
				}
				else
				{
					ReversUV(uvs,top,newUVS,false,true);
				}
			}
			else if (faceType == CubeFaceType.Bottom)
			{
				Vector2[] newUVS = GetUVS(bottomPoint.x, bottomPoint.y);
				for (int i = 0; i < 4; i++)
				{
					uvs[bottom[i]] = newUVS[i];
				}
				if (!bottomTexture.reversHorizontal && !bottomTexture.reversVertical)
				{
					for (int i = 0; i < 4; i++)
					{
						uvs[bottom[i]] = newUVS[i];
					}
				}
				else if (bottomTexture.reversHorizontal && bottomTexture.reversVertical)
				{
					ReversUV(uvs,bottom,newUVS,true,true);
				}
				else if (bottomTexture.reversHorizontal)
				{
					ReversUV(uvs,bottom,newUVS,true,false);
				}
				else
				{
					ReversUV(uvs,bottom,newUVS,false,true);
				}
			}
			else if (faceType == CubeFaceType.Left)
			{
				Vector2[] newUVS = GetUVS(leftPoint.x, leftPoint.y);
				for (int i = 0; i < 4; i++)
				{
					uvs[left[i]] = newUVS[i];
				}
				if (!leftTexture.reversHorizontal && !leftTexture.reversVertical)
				{
					for (int i = 0; i < 4; i++)
					{
						uvs[left[i]] = newUVS[i];
					}
				}
				else if (leftTexture.reversHorizontal && leftTexture.reversVertical)
				{
					ReversUV(uvs,left,newUVS,true,true);
				}
				else if (leftTexture.reversHorizontal)
				{
					ReversUV(uvs,left,newUVS,true,false);
				}
				else
				{
					ReversUV(uvs,left,newUVS,false,true);
				}
			}
			else if (faceType == CubeFaceType.Right)
			{
				Vector2[] newUVS = GetUVS(rightPoint.x, rightPoint.y);
				for (int i = 0; i < 4; i++)
				{
					uvs[right[i]] = newUVS[i];
				}
				if (!rightTexture.reversHorizontal && !rightTexture.reversVertical)
				{
					for (int i = 0; i < 4; i++)
					{
						uvs[right[i]] = newUVS[i];
					}
				}
				else if (rightTexture.reversHorizontal && rightTexture.reversVertical)
				{
					ReversUV(uvs,right,newUVS,true,true);
				}
				else if (rightTexture.reversHorizontal)
				{
					ReversUV(uvs,right,newUVS,true,false);
				}
				else
				{
					ReversUV(uvs,right,newUVS,false,true);
				}
			}
		}

		public void GenMergedTexture()
		{
			mesh = GetComponent<MeshRenderer>();
			Texture2D[] texture2Ds = new Texture2D[6];
			texture2Ds[0] = topTexture.texture;
			texture2Ds[1] = bottomTexture.texture;
			texture2Ds[2] = leftTexture.texture;
			texture2Ds[3] = rightTexture.texture;
			texture2Ds[4] = frontTexture.texture;
			texture2Ds[5] = backTexture.texture;
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