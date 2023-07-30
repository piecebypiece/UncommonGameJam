using DG.Tweening;
using UnityEngine;

public class UIRadar : MonoBehaviour
{
    public GameObject Image;
    PlayerController pc;
    [SerializeField]
    bool isClosedSomeOne;
    bool isVeryClosedSomeOne;
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

        bool close = isVeryClosedSomeOne;
        isVeryClosedSomeOne = PlayManager.Inst.CountingAroundPlayer(pc, 25f) > 0;
        if (isVeryClosedSomeOne != close)
        {
            if (isVeryClosedSomeOne)
            {
                pc.LightPillar.SetActive(true);
                pc.LightPillar.transform.DOScaleY(500, 2f);
            }
            else
            {
                DOTween.Kill(pc.LightPillar.transform);
                pc.LightPillar.transform.localScale = new Vector3(0.5f, 0, 0.5f);
                pc.LightPillar.SetActive(false);
            }
                
        }
    }
}