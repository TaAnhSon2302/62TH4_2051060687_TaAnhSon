using System;
using UnityEngine.EventSystems;

using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private Camera worldCamera;

    public Vector3 mouseWorldPosition;
    public Vector3 arrowDirection;
    public bool isMouseClick = false;
    public bool isMouseClick2 = false;
    public Action onFire;
    public Action onFire2;
    public bool Ability1Button = false;
    public bool Ability2Button = false;
    public bool Ability3Button = false;
    public bool isOnPauseState = false;
    public float timeBetweenClick = 0.5f;
    private int clickMouseRight = 0;
    private bool isRightTimeCheckAllowed = true;
    private float firstRightClickTime = 0;
    private int clickMouseLeft = 0;
    private bool isLeftTimeCheckAllowed = true;
    private float firstLeftClickTime = 0;
    public Action onDoubleClickLeft;
    public Action onDoubleClickRight;
    // public Action Ability1;
    // public Action Ability2;
    // public Action Ability3;

    private void Update() {
        if(!isOnPauseState){
            GetMousePosition();
            GetArrowButton();
            GetMouseClick();
            GetMouseHold();
            GetDoubleClickRight();
            GetDoubleClickLeft();
            GetAbilityButtonDown();
        }
    }
    public Vector3 GetMousePosition(){
        mouseWorldPosition = worldCamera.ScreenToWorldPoint(Input.mousePosition);
        return worldCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    public Vector3 GetWASD(){

        return Vector3.zero;
    }
    public Vector3 GetArrowButton(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        arrowDirection = new Vector3(horizontal, vertical, 0f);
        return arrowDirection.normalized;
    }
    public void GetMouseClick(){
        isMouseClick = Input.GetButtonDown("Fire1");
        if(isMouseClick)
            onFire?.Invoke();
        isMouseClick2 = Input.GetButtonDown("Fire2");
        if(isMouseClick2)
            onFire2?.Invoke();
    }
    public void GetMouseHold(){
        bool isMouseHold = Input.GetMouseButton(0);
        if(isMouseHold)
            onFire?.Invoke();
        bool isMouseHold2 = Input.GetMouseButton(1);
        if(isMouseHold2)
            onFire2?.Invoke();
    }
    public void GetAbilityButtonDown(){
        Ability1Button = Input.GetKeyDown(KeyCode.E);
        Ability2Button = Input.GetKeyDown(KeyCode.R);
        Ability3Button = Input.GetKeyDown(KeyCode.Q);
    }


    public void GetDoubleClickRight(){
        if(Input.GetButtonUp("Fire2"))
            clickMouseRight++;
        if(clickMouseRight == 1 && isLeftTimeCheckAllowed){
            firstRightClickTime = Time.time;
            StartCoroutine(DetectDoubleRightMouseClick());
        }

        IEnumerator DetectDoubleRightMouseClick(){
            isLeftTimeCheckAllowed = false;
            while(Time.time < firstRightClickTime+timeBetweenClick){
                if(clickMouseRight == 2){
                    onDoubleClickRight?.Invoke();
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            clickMouseRight = 0;
            isLeftTimeCheckAllowed = true;
        }
    }
     public void GetDoubleClickLeft(){
        if(Input.GetButtonUp("Fire1"))
            clickMouseLeft++;
        if(clickMouseLeft == 1 && isRightTimeCheckAllowed){
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleLeftMouseClick());
        }

        IEnumerator DetectDoubleLeftMouseClick(){
            isRightTimeCheckAllowed = false;
            while(Time.time < firstLeftClickTime+timeBetweenClick){
                if(clickMouseLeft == 2){
                    onDoubleClickLeft?.Invoke();
                    break;
                }
                yield return null;
            }
            clickMouseLeft = 0;
            isRightTimeCheckAllowed = true;
        }
    }
}
