using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNameManager : Singleton<StoryNameManager>
{
    [Tooltip("The name of the story in the assetbundle")]
    public string storyName;
}
