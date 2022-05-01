using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    GameObject[] itemPos;
    // Start is called before the first frame update

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public int ItemNums = 5;


    public float CreateItemTime = 10;//ÿ��10������һ��
    public float ItemClock = 10;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ItemClock < 0)
        {
            CreateItem();
            CreateItemTime += 5; //�´����ɵ��ߵ�ʱ�����
            ItemClock = CreateItemTime;
        }else
        {
            ItemClock -= Time.deltaTime;
        }


    }

    void CreateItem()
    {
        Vector3[] ItemPositions = new Vector3[10]; //������10��λ�� �����ñ߽�����ע���ֹԽ��
        int num = 0;
        foreach (Transform child in this.gameObject.transform)
        {
            ItemPositions[num] = child.gameObject.transform.position;
            num += 1;
        }

        int posNum = Random.Range(0, num);//���ѡ��һ��λ��
        int ItemNum = Random.Range(1, ItemNums+1);//���ѡ��һ������
        GameObject itemObj;
        switch (ItemNum)
        {
            case 1:
                itemObj = Instantiate(item1, ItemPositions[posNum], Quaternion.Euler(270f, 0.0f, 0.0f));//���ɵ���
                break;
            case 2:
                itemObj = Instantiate(item2, ItemPositions[posNum], Quaternion.Euler(270f, 0.0f, 0.0f));//���ɵ���
                break;
            case 3:
                itemObj = Instantiate(item3, ItemPositions[posNum], Quaternion.Euler(270f, 0.0f, 0.0f));//���ɵ���
                break;
            case 4:
                itemObj = Instantiate(item4, ItemPositions[posNum], Quaternion.Euler(270f, 0.0f, 0.0f));//���ɵ���
                break;
            case 5:
                itemObj = Instantiate(item5, ItemPositions[posNum], Quaternion.Euler(270f, 0.0f, 0.0f));//���ɵ���
                break;
        }

        print("�����������" + ItemPositions[posNum]);
    }
    
}
