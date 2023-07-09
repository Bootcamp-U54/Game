using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButtonManager : MonoBehaviour
{
    public Image mainImage,storyImage, trackImage, controllerImage;
    public GameObject storyBook, trackBook, controllerBook;
    public int track,story,controller;

    private void Start()
    {
        SetAllImageVisibility(false);
        SetAllBookActive(false);
        mainImage.enabled = true;
    }
    public void OnClickStoryButton(int index)
    {
        SetAllImageVisibility(false);
        SetAllBookActive(false);

        storyImage.enabled = true;
        storyBook.SetActive(true);
        story=index;
    }

    public void OnClickTrackButton(int index)
    {
        SetAllImageVisibility(false);
        SetAllBookActive(false);

        trackImage.enabled = true;
        trackBook.SetActive(true);
        track = index;

    }

    public void OnClickControllerButton(int index)
    {
        SetAllImageVisibility(false);
        SetAllBookActive(false);

        controllerImage.enabled = true;
        controllerBook.SetActive(true);
        controller = index; 
    }

    private void SetAllImageVisibility(bool visible)
    {
        mainImage.enabled = visible;
        storyImage.enabled = visible;
        controllerImage.enabled = visible;
        trackImage.enabled = visible;
    }

    private void SetAllBookActive(bool active)
    {
        storyBook.SetActive(active);
        controllerBook.SetActive(active);
        trackBook.SetActive(active);
    }



}
