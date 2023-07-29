using DG.Tweening;
using UnityEngine;

public class UIRadar : MonoBehaviour
{
    public GameObject Image;
    PlayerController pc;
    [SerializeField]
    bool isClosedSomeOne;
    public void Start()
    {
        pc = PlayManager.Inst.GetMineController();
    }
    public void Update()
    {
        bool before = isClosedSomeOne;
        isClosedSomeOne = PlayManager.Inst.CountingAroundPlayer(pc, UCDefine.RaderDistance) > 0;

        if(isClosedSomeOne!= before)
        {
            if(isClosedSomeOne) 
                Image.transform.DOScale(new Vector2(2, 2), 1).SetLoops(-1, LoopType.Yoyo);
            else
                DOTween.Kill(Image.transform);
            Image.SetActive(isClosedSomeOne);
        }        
        
    }
}