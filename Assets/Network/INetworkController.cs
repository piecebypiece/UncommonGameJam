using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 게임 실행도중 네트워크 처리할 게 있다면 이 인터페이스의 함수를 호출한다.
// 구현상 필요없는 쪽이면 함수가 비어있을 수도 있음.
public interface INetworkController
{
    public void OnItem();

    public void SpawnPlayer();

    public void CompleteSpwan();

    public void SpawnItem();

    public void SendStemp();
}
