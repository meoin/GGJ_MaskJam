using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAnimaticImage : MonoBehaviour
{
    public List<string> AnimaticNames;
    public List<Sprite> AnimaticImages;

    public Sprite GetImage(string animatic) 
    {
        Debug.Log($"Finding image for: '{animatic}'");
        int index = AnimaticNames.FindIndex(x => x.Contains(animatic));

        if (index < 0) index = 0;

        return AnimaticImages[index];
    }
}
