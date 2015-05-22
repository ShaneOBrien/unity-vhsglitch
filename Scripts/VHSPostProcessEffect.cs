using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent (typeof(Camera))]

public class VHSPostProcessEffect : PostEffectsBase{
	Material m;
	public Shader shader;

	public Texture[] vhsFrames;
	public float videoTime = 0;

	float yScanline, xScanline;

	public void Start() {
		m = new Material(shader);
		m.SetTexture("_VHSTex", vhsFrames[0]);
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination){
		yScanline += Time.deltaTime * 0.1f;
		xScanline -= Time.deltaTime * 0.1f;
		videoTime += Time.deltaTime *100;
		
		if (videoTime >= vhsFrames.Length){
			videoTime = 0;
		}
		
		m.SetTexture("_VHSTex", vhsFrames[(int)videoTime]);
		if(yScanline >= 1){
			yScanline = Random.value;
		}
		if(xScanline <= 0 || Random.value < 0.05){
			xScanline = Random.value;
		}
		m.SetFloat("_yScanline", yScanline);
		m.SetFloat("_xScanline", xScanline);
		Graphics.Blit(source, destination, m);
	}
}