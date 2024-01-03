﻿using UnityEngine;
using System.Collections;

namespace AquariusMax.Demo
{
public class TextureScroll : MonoBehaviour {
	public float scrollSpeed = 0.5F;
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		float offset = Time.time * scrollSpeed;
		rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
}