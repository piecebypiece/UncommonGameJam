using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizeTest : MonoBehaviour
{
    public string testKey;

    public Locale baselocale;

    public Locale myLocale;


    [ContextMenu("Testing")]
    public void test()
    {
        Debug.Log(Helper.Localize(myLocale, testKey)); 
    }

    
}
