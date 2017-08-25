using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotopage : MonoBehaviour {

    private int pageTarget = 0;

	public void pageLoad ()
    {
        Debug.Log(gameObject.name);
        //PageManager.instance.GoToPage(pageTarget);
    }
    public void setPageTarget(int page)
    {
        //pageTarget = page;
		Debug.Log("Working");
    }
}
