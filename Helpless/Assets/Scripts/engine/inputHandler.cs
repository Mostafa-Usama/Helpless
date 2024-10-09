using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputHandler : MonoBehaviour{

    inputConstants c = new inputConstants();

    private void Start() {

        }

    bool canMove(){
        return true;
    }

    public float getHorizontal() => canMove() ? Input.GetAxis(c.horizontal) : 0 ;
    
    public float getVertical() =>  canMove() ? Input.GetAxis(c.vertical) : 0 ;

    public float getleftClick() =>  Input.GetAxis(c.leftClick) ;
    public float getRightClick() =>  Input.GetAxis(c.rightClick) ;
    public float getMouseX() =>  Input.GetAxis(c.mouseX) ;
    public float getMouseY() =>  Input.GetAxis(c.mouseY) ;
    public float getJump() =>  Input.GetAxis(c.jump) ;
    public float getReload() =>  Input.GetAxis(c.reload) ;    
    public float getCrouch() =>  Input.GetAxis(c.crouch) ;
    public float getLeftShift() =>  Input.GetAxis(c.leftShift) ;

}
