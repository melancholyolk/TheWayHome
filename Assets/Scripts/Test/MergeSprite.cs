using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSprite : MonoBehaviour
{
    public Texture2D topTexture;
    public Texture2D bottomTexture;
    public Texture2D leftTexture;
    public Texture2D rightTexture;
    public Texture2D frontTexture;
    public Texture2D backTexture;

    private MeshRenderer mesh;
	private void Update()
	{
		
        mesh = GetComponent<MeshRenderer>();
        Texture2D[] texture2Ds = new Texture2D[6];
        texture2Ds[0] = topTexture;
        texture2Ds[1] = bottomTexture;
        texture2Ds[2] = leftTexture;
        texture2Ds[3] = rightTexture;
        texture2Ds[4] = frontTexture;
        texture2Ds[5] = backTexture;
        mesh.material.SetTexture("_BaseMap", MergeMoreTex(texture2Ds));
    }
	public static Texture2D MergeMoreTex(Texture2D[] texs)
    {
        if (texs.Length < 1) return null;
        Texture2D nTex = new Texture2D(4096, 4096, TextureFormat.ARGB32, true);
        Color[] colors = new Color[nTex.width * nTex.height];
        int startw, starth;
        startw = 0;//����д��ƫ��
        starth = 0;//����д��ƫ��
        //���¼�����������������ܻ���ִ󲿷ֿ��࣬����ļ������������Լ�������û�в��Թ��������������Ҫ�����޸�
        //������������������
        int wcnt = Mathf.CeilToInt(Mathf.Sqrt(texs.Length / 1.8f));
        //�������
        int hcnt = Mathf.FloorToInt(wcnt * (1280f / 720f));
        //���Ÿ߶�
        int oneh = Mathf.FloorToInt(4096 / hcnt);
        //���ſ��
        int onew = Mathf.FloorToInt(4096 / wcnt);
        for (int i = 0; i < texs.Length; i++)
        {
            for (int h = 0; h < oneh; h++)
            {
                for (int w = 0; w < onew; w++)
                {
                    Color color = texs[i].GetPixelBilinear((float)w / onew, (float)h / oneh);
                    int index = h * nTex.width + w + startw + (starth * 4096);
                    if (index >= colors.Length)
                    {
                        Debug.LogError("����Խ��");
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
}
