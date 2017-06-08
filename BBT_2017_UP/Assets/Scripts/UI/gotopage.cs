using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotopage : MonoBehaviour {

	public void pageLoad (int page)

    {
        PageManager.instance.GoToPage(page);
    }

}
