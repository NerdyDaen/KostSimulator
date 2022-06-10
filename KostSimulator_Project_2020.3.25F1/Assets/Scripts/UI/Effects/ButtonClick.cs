using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public void Scale(float scale)
    {
        transform.localScale = new Vector2(1 / scale, 1 * scale);
    }

    public void Scene(string scene)
    {
        Application.LoadLevel(scene);
    }
}
