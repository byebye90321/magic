using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adventureLock : MonoBehaviour {

    public GameObject Obj00;
    public GameObject Obj01;
    public GameObject Obj02;
    public GameObject Obj03;
    public GameObject Obj01redflower;
    public GameObject Obj01blueflower;
    public GameObject Obj01redfairy;
    public GameObject Obj01bluefairy;
    public GameObject Obj02rightclock;
    public GameObject Obj02falseclock;
    public GameObject HE1;
    public GameObject BE1;
    public GameObject BE2;
    public GameObject HE2;
    public GameObject BE3;

    void Start()
    {
        int ad00;
        int ad01;
        int ad02;
        int ad03;
        int ad01_flower_red;
        int ad01_flower_blue;
        int ad01_fairy_red;
        int ad01_fairy_blue;
        int ad02_clock_right;
        int ad02_clock_false;
        int ad01_HE1;
        int ad01_BE1;
        int ad02_BE2;
        int ad03_HE2;
        int ad03_BE3;

        ad00 = StaticObject.ad0;
        ad01 = StaticObject.ad1;
        ad02 = StaticObject.ad2;
        ad03 = StaticObject.ad3;
        ad01_flower_red = StaticObject.ad1_flower_red;
        ad01_flower_blue = StaticObject.ad1_flower_blue;
        ad01_fairy_red = StaticObject.ad1_fairy_red;
        ad01_fairy_blue = StaticObject.ad1_fairy_blue;
        ad02_clock_right = StaticObject.ad2_clock_right;
        ad02_clock_false = StaticObject.ad2_clock_false;
        ad01_HE1 = StaticObject.ad1_HE1;
        ad01_BE1 = StaticObject.ad1_BE1;
        ad02_BE2 = StaticObject.ad2_BE2;
        ad03_HE2 = StaticObject.ad3_HE2;
        ad03_BE3 = StaticObject.ad3_BE3;

        if (ad00 == 1)
            Obj00.SetActive(true);
        if (ad01 == 1)
            Obj01.SetActive(true);
        if (ad02 == 1)
            Obj02.SetActive(true);
        if (ad03 == 1)
            Obj03.SetActive(true);

        if (ad01_flower_red == 1)
            Obj01redflower.SetActive(true);
        if (ad01_flower_blue == 1)
            Obj01blueflower.SetActive(true);
        if (ad01_fairy_red == 1)
            Obj01redfairy.SetActive(true);
        if (ad01_fairy_blue == 1)
            Obj01bluefairy.SetActive(true);
        if (ad02_clock_right == 1)
            Obj02rightclock.SetActive(true);
        if (ad02_clock_false == 1)
            Obj02falseclock.SetActive(true);

        if (ad01_HE1 == 1)
            HE1.SetActive(true);
        if (ad01_BE1 == 1)
            BE1.SetActive(true);
        if (ad02_BE2 == 1)
            BE2.SetActive(true);
        if (ad03_HE2 == 1)
            HE2.SetActive(true);
        if (ad03_BE3 == 1)
            BE3.SetActive(true);
    }
}
