#pragma strict


function Update () {

if (Input.GetKey("1"))
    { 
    animation.Play("out-snap-open");
    }

if (Input.GetKey("2"))
    {
    animation.Play("out-slam-shut");
    }

if (Input.GetKey("3"))
    {
    animation.Play("out-open-slowly");
    } 

if (Input.GetKey("4"))
    {
    animation.Play("out-close");
    }

if (Input.GetKey("5"))
    {
    animation.Play("in-snap-open");
    }

if (Input.GetKey("6"))
    {
    animation.Play("in-slam-shut");
    }

if (Input.GetKey("7"))
    {
    animation.Play("in-open-slowly");
    }        

if (Input.GetKey("8"))
    {
    animation.Play("in-close");
    }

}