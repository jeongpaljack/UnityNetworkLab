using UnityEngine;

namespace Chapter.Singleton                         // Chapter 파일 안의 Singleton 파일이라는 의미
{
    public class Singleton<T> :                     // 제네릭 클래스, T의 의미는 나중에 형식을 지정하겠다는 뜻
        MonoBehaviour where T : Component           // 제네릭의 조건, T의 타입은 반드시 Unity의 Component 혹은 그 하위 타입이어야 함
    {

        private static T _instance;                 // private static T 형식의 _instance 선언 - 프로그램에서 하나만 존재하지만, 외부에서 접근 불가능한 함수

        public static T Instance                    // static 형식의 Instance 선언         -> 해당 싱글톤의 타입 반환 함수       -> 싱글톤의 인스턴스를 사용할 때 사용
        {
            get
            {
                if (_instance == null)              // Awake에서 저장한 T 타입, 즉 _instance의 형식이 없을 경우
                {
                    _instance = FindFirstObjectByType<T>();     // 이미 T가 존재할 경우 이것으로 대체

                    if (_instance == null)                      // T가 최종적으로 존재하지 않을 경우
                    {
                        GameObject obj = new GameObject();      // 새로운 오브젝트 생성
                        obj.name = typeof(T).Name;              // 오브젝트의 이름(name)을 클래스의 이름(Name)으로 초기화
                        _instance = obj.AddComponent<T>();      // _instance의 타입을 obj의 T 이름의 컴포넌트로 정의
                    }
                }

                return _instance;                               // Instance의 값을 _instance로 지정
            }
        }

        public virtual void Awake()                             // 오브젝트가 활성화될 때, 제일 먼저 실행    -> 싱글톤이 하나만 있도록 하는 함수
        {
            if (_instance == null)                              // _instance가 없을 경우, 즉 싱글톤 인스턴스가 없을 경우
            {
                _instance = this as T;                          // 해당 인스턴스를 T타입으로 캐스팅하여 저장
                DontDestroyOnLoad(gameObject);                  // 씬 전환 시에도 싱글톤 인스턴스가 파괴되지 않게 유지
            }
            else
            {
                Destroy(gameObject);                            // 이미 인스턴스가 있으면 새로운 게임 오브젝트를 삭제시킴
            }
        }
    }
}