2024-07-16
assetName : CrogenPooling
version : 1.2.0

<업데이트>
-에셋 이름 ObjectPooling -> CrogenPooling 으로 변경

<오브젝트 풀링 사용 방법>
1. 새로운 오브젝트를 만든 후 "PoolManager" 컴포넌트를 추가한다.
2. "New" 버튼을 클릭하여 새로운 PoolBase를 만든다.
3. "Clone"으로 PoolBase를 복제 가능하다 (이를 서포트하는 기능이 구현되어 있지 않으니 사용을 권장하지 않는다)
4. 풀링할 오브젝트에 붙혀있는 스크립트에 "IPoolingObject"를 추가하여 내부를 구현한다.
5. 풀링할 오브젝트를 PoolBase에 추가하고 "Generate Enum"를 클릭하면 PoolType이 업데이트되어 스크립트에서 사용이 가능하다.
6. 풀링 오브젝트를 Pop할 때는 gameObject.Pop(PoolType type, Transform parent, Vector3 pos, Quaternion rot)를 호출, 
   Push할 땐 (풀링 오브젝트).Push()를 호출하면 된다.