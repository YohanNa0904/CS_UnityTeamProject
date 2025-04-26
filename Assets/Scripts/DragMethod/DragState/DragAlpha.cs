using UnityEngine;

public class DragAlpha : DropStateGravity
{
    Material[] oriMateArray = null; 
    [SerializeField] Material dragAlpha; //드래그하는 동안 바꿔줄 마테리얼 설정
    Renderer[] rendArray = null;
    [SerializeField, Range(0.0f, 1.0f)] float alphaValue = 0.4f; // 바꿀 마테리얼의 알파값 설정
    protected override void OnDragSet()
    {
        base.OnDragSet();
        rendArray = GetComponentsInChildren<Renderer>();
        oriMateArray = new Material[rendArray.Length];

        for(int i = 0; i < rendArray.Length; i++)
        {
            oriMateArray[i] = rendArray[i].material;
            rendArray[i].material = dragAlpha;
        }
        // 기존 마테리얼을 oriMateArray에 저장하고 마테리얼을 dragAlpha로 변경함

        //dragAlpha.SetColor("_Color",Color.green);
        dragAlpha.color = Color.green;
        dragAlpha.SetFloat("_Alpha", alphaValue);
    }

    protected override void DragAlphaTF(bool tf)
    {
        if(tf && dragAlpha.color != Color.green) 
            dragAlpha.color = Color.green;
        // 드래그한 오브젝트를 놓을 수 있으면 색을 녹색으로 변경
        else if(!tf && dragAlpha.color != Color.red) 
            dragAlpha.color = Color.red;
        //놓을 수 없으면 색을 빨간 색으로 변경
    }

    protected override void EndDragSet()
    {
        for (int i = 0; i < rendArray.Length; i++)
        { 
            rendArray[i].material = oriMateArray[i];
        }
        base.EndDragSet();
        // 드래그가 끝나면 마테리얼을 기존 마테리얼로 되돌림
    }
}
