using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingleTon<GameManager> {

    [Range(0.01f, 10.0f)]
    public float cameraSensivity = 1.6f;
    public bool isEditMode = false;

    //Material FadeIn/Out
	public IEnumerator MaterialFadeIn(Material material)
    {
        for (float i = 1f; i >= 0; i -= Time.deltaTime)
        {
            Color color = new Vector4(1, 1, 1, i);
            material.color = color;
            yield return null;
        }
    }
    public IEnumerator MaterialFadeOut(Material material)
    {
        for (float i = 0f; i <= 1; i += Time.deltaTime)
        {
            Color color = new Vector4(1, 1, 1, i);
            material.color = color;
            yield return null;
        }
    }

    //Image FadeIn/Out
    public IEnumerator ImageFadeIn(Image image)
    {
        for (float i = 1f; i >= 0; i -= Time.deltaTime)
        {
            Color color = new Vector4(1, 1, 1, i);
            image.color = color;
            yield return null;
        }
    }
    public IEnumerator ImageFadeOut(Image image)
    {
        for (float i = 0f; i <= 1; i += Time.deltaTime)
        {
            Color color = new Vector4(1, 1, 1, i);
            image.color = color;
            yield return null;
        }
    }
}
