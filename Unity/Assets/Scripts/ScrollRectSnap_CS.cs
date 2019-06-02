﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap_CS : MonoBehaviour
{
    public RectTransform panel; //To hold the ScrollPanel
    public Image[] img;
    public RectTransform center; //Center to compare the distance for each image

    public float[] distance; //All images' distance to the center
    public float[] distReposition;
    private bool dragging = false; //Will be true, while we drag the panel
    private int imgDistance; //Will hold the distance between images
    private int minimageNum; //To hold the number of the image, with smallest distance to center
    private int imgLength;

    void Start()
    {
        imgLength = img.Length;
        distance = new float[imgLength];
        distReposition = new float[imgLength];

        //Get distance between images
        imgDistance = (int) Mathf.Abs(img[1].GetComponent<RectTransform>().anchoredPosition.x -
                                       img[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    void Update()
    {
        for (int i = 0; i < img.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - img[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);
            
            if (distReposition[i] > 2000)
            {
                float curX = img[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = img[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2 (curX + (imgLength * imgDistance), curY);
                img[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
            if (distReposition[i] < -2000)
            {
                float curX = img[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = img[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX - (imgLength * imgDistance), curY);
                img[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
        }  

        float minDistance = Mathf.Min(distance); //Get the min distance

        for (int a = 0; a < img.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minimageNum = a;
            }
        }
            if (!dragging)
            {
                //  LerpToimg(minimageNum * -imgDistance);
                LerpToimg(-img[minimageNum].GetComponent<RectTransform>().anchoredPosition.x);
            }

    }

    void LerpToimg(float position)
    {
        float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
