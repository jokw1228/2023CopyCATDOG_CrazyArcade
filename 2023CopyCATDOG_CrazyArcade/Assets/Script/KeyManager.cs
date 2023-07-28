using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KeyAction { UP1, DOWN1, LEFT1, RIGHT1, BALL1, ITEM1, UP2, DOWN2, LEFT2, RIGHT2, BALL2, ITEM2, KEYCOUNT }
//열거형의 실제 사용하는 값의 개수와 같음.​
public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }
//KeyAction과 KeyCode를 key값과 value값으로 한 딕셔너리 생성
public class KeyManager : Singleton<KeyManager>
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.LeftShift, KeyCode.LeftControl, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightShift, KeyCode.Slash };
    protected override void Awake()
    {
        base.Awake();

        KeyCode outValue;
        if(!(KeySetting.keys.TryGetValue(KeyAction.UP1,out outValue)))
        {
            for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
            {
                KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
                //for문을 통해서 defaultKeys에 저장된 배열을 순서대로 KeyAction에 값 추가
            }
        }
    }
    private void OnGUI()
    //OnGUI()는 GUI, 키  입력 등의 이벤트가 발생할 때 호출됩니다.
    {
        Event keyEvent = Event.current;
        //Event 클래스로 현재 실해되는 Event를 불러옵니다.
        if (keyEvent.isKey)//키가 눌렸을 때만 실행.
        {
            //이벤트의 keyCode로 현재 눌린 키보드의 값을 알 수 있습니다.
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode; //변수 key는 int형이므로 KeyAction으로 캐스팅.
            key = -1; //keys를 바꾼뒤에도 key를 다시 -1로 만듦.
        }
    }
    int key = -1; // key 변수를 -1로 초기화.
    public void ChangeKey(int num)
    {
        key = num;
    }
}