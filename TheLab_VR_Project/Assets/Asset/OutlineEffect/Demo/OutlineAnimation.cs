using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

namespace cakeslice
{
    public class OutlineAnimation : MonoBehaviour
    {
        bool pingPong = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Color c = GetComponent<OutlineEffect>().lineColor0;

            if(pingPong)
            {
                c.a += Time.deltaTime;

                if(c.a >= 1.0f)
                    pingPong = false;
            }
            else
            {
                c.a -= Time.deltaTime;

                if(c.a <= 0.3f)
                    pingPong = true;
            }

            c.a = Mathf.Clamp(c.a,0.3f, 1.0f);
            GetComponent<OutlineEffect>().lineColor0 = c;
            GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();
        }
    }
}