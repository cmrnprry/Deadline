using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public Transform screen1;
    public Transform screen2;
    public float lookAtSpeed = 1f;
    public float sensitivity = 1;
    public float smoothing = 2;
    public bool canLook = true;


    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        if(canLook){
          Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        // Get smooth mouse look.
        if(canLook){
          Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
          appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
          currentMouseLook += appliedMouseDelta;
          currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

          // Rotate camera and controller.
          transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
          character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
        }

        if(Input.GetButtonDown("Pressed E")){
          if(canLook){StartCoroutine(LookAtScreenOne());}
          ToggleMode();
        }



    }


    public void ToggleMode(){
      if(canLook){
        DisableLooking();
      }
      else{
        EnableLooking();
      }
    }

    public void DisableLooking(){
      canLook = false;
      Cursor.lockState = CursorLockMode.Confined;
      Cursor.visible = true;
    }

    public void EnableLooking(){
      canLook = true;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    public IEnumerator LookAtScreenOne(){
      while(true){
        Quaternion lastRotation = transform.rotation;
        var step = lookAtSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, screen1.rotation, step);
        yield return null;
        if(lastRotation == transform.rotation){
          break;
        }
      }

    }

    public IEnumerator LookAtScreenTwo(){
      while(true){
        Quaternion lastRotation = transform.rotation;
        var step = lookAtSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, screen2.rotation, step);
        yield return null;
        if(lastRotation == transform.rotation){
          break;
        }
      }
    }

}
