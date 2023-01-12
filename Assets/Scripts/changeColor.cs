using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{

    public static System.Action change_b;
    public static System.Action change_w;

    private Renderer color_object;

    void Start()
    {
        color_object = gameObject.GetComponent<Renderer>();
    }

    private void Awake()
    {
        change_b = () => { color_black(); };
        change_w = () => { color_white(); };
    }

    void color_black()
    {

        color_object.material.color = Color.black;

    }

    void color_white()
    {
        color_object.material.color = Color.white;
    }


}
