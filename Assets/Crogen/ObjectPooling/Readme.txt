2024-01-13
version : 1.0.2

<업데이트> 
PoolDeath 스크립트를 추가하여 Pop 동작 시행 이후 자동으로 Pool에 들어가는 오브젝트(ex : 이펙트)들을 간편하게 Pooling할 수 있도록 함

<오브젝트 풀링 사용 방법>
1. Project 창에 우클릭을 누르고 Crogen/ObjectPooling/PoolingBase 경로로 PoolingBase를 생성한다.
2. PoolingBase를 클릭하고 Inspector 창에서 pairs에 요소를 추가한다.
3. 추가된 pairs 요소의 Prefab Type Name에 Pop으로 불러올 문자열 값을 입력한다.
4. Prefab에 풀링할 오브젝트 Prefab를 넣는다.
5. PoolCount에 초기에 생성할 PoolingObject의 개수를 지정한다. 
(Pop된 오브젝트의 개수가 PoolCount를 넘어서면 자동으로 오브젝트를 추가해 PoolingQueue에 넣어주니 걱정말자) 