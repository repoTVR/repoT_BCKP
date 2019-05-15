using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Add this script to the object that has a Slider component
/// attached to it.
/// </summary>
public class Fill_On_Gaze : MonoBehaviour
{
    //Public Variables.
    [Header("How lond does it take to fill the slider:")]
    public float fillTime; //How long it takes to fill the slider.

    [Space][Space]
    [Header("Do you want the slider to be disabled on start?")]
    public bool disableOnStart;

    [Space][Space]
    [Header("Place the button we want to trigger on filling the slider:")]
    public Button usedButton; //The button we want to use for function invoking when the slider is filled.

    [Space]
    [Space]
    [Header("Animation Settings")]
    public bool dontUseAnimation; //By default, we use an animation.
    public enum AnimationOptions { Shake, ColorChange, Rescale } //Animation variations.
    public AnimationOptions animationToPlay = AnimationOptions.ColorChange;

    [Header("Place the animator playing the button animations.")]
    public Animator animator;

    //Private Variables.
    private Slider mySlider; //The slider we work with.
    private float timer; //We use this value in our coroutines.
    private bool gazedAt; //Bool we use for checking if we are looking at something.
    private Coroutine fillBarRoutine;

    private void Start()
    {
        mySlider = GetComponent<Slider>();

        //We check if we have a Slider component attached.
        if (mySlider == null)
        {
            Debug.Log("No slider was found. Please attach this script only to objects that have a Slider component.");
        }

        //We check if we need to disable the slider on start.
        if (disableOnStart)
        {
            mySlider.gameObject.SetActive(false);
        }
    }

    //Method we invoke when our gaze is on the object.
    public void PointerEnter()
    {
        if (disableOnStart)
        {
            mySlider.gameObject.SetActive(true);
        }
       
        gazedAt = true;
        usedButton.interactable = false; //We disable the button itself to prevent unwanted issues.
        fillBarRoutine = StartCoroutine(FillBar());
    }

    //Method we invoke when our gaze is away from the object.
    public void PointerExit()
    {
        if (disableOnStart)
        {
            mySlider.gameObject.SetActive(false);
        }

        gazedAt = false;

        //Stop the routine if we have started it.
        if (fillBarRoutine != null)
        {
            StopCoroutine(fillBarRoutine);
        }

        //Resetting values.
        timer = 0f;
        mySlider.value = 0f;
    }

    //Coroutine we use for changing the slider value.
    private IEnumerator FillBar()
    {
        timer = 0f;

        while (timer < fillTime)
        {
            timer += Time.deltaTime;
            mySlider.value = timer / fillTime;
            yield return null;

            //If we are still looking at the object.
            if (gazedAt)
                continue;

            timer = 0f;
            mySlider.value = 0f;

            yield break;
        }

        OnBarFilled();
    }

    //Method we use for finalizing the routine.
    private void OnBarFilled()
    {
        mySlider.value = 0f;

        //Depending on the animation settings, we play it.
        if(animationToPlay == AnimationOptions.ColorChange)
        {
            animator.Play("ColorChange");
        }
        else if(animationToPlay == AnimationOptions.Shake)
        {
            animator.Play("Shake");
        }
        else if(animationToPlay == AnimationOptions.Rescale)
        {
            animator.Play("Rescale");
        }

        //We use the button as we are pressing it.
        usedButton.onClick.Invoke();
    }
}