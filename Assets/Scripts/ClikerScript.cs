using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class ClikerScript : MonoBehaviour
{
    public int cash = 0;
    public int addCash = 1;
    public int AutoLootingCash = 10;
    public Text lblCash;

    public List<GameObject> shop;
    public Button btn10;
    public Button btn100;
    public Button btn1000;

    // Start is called before the first frame update
    void Start()
    {
        ShowCash();
        ShowBar(false);
        AutoLootingAsync();
        FillLblCashAsync();
    }

    public void AddCash()
    {
        cash += addCash;
    }

    public void ShowCash()
    {
        lblCash.text = $"<color='#E5E5E5'>{cash}</color>";
    }

    public void OilClik()
    {
        AddCash();
        ShowCash();
    }

    public void ShowBar(bool active)
    {
        foreach (var item in shop)
        { 
            item.SetActive(active);
        }

        if (cash >= 10)
        {
            btn10.enabled = true;
        }
        else
        {
            var colorBlock = btn10.colors;
            colorBlock.disabledColor = Color.red;
            btn10.colors = colorBlock;

            btn10.enabled = false;

        }
        if (cash >= 100)
        {
            btn100.enabled = true;
        }
        else
        {
            btn100.enabled = false;
        }
        if(cash >= 1000)
        {
            btn1000.enabled = true;
        }
        else
        {
            btn1000.enabled = false;
        }
    }
    public void AddOne()
    {
        PriceClick(10, 1);
    }
    public void AddTen()
    {
        PriceClick(100, 10);
    }
    public void AddHundred()
    {
        PriceClick(1000, 100);
    }
    public void Close(bool active)
    {
        foreach (var item in shop)
        {
            item.SetActive(active);
        }
    }

    public void PriceClick(int price, int addPrice)
    {
        if (cash >= price)
        {
            cash -= price;
            addCash += addPrice;
        }
        ShowCash();
    }

    public async void AutoLootingAsync() // async �� ������ ����� �����������, �� ������ ���������� ��� ����� ���� ��������� ��������� � await
    {
        await Task.Run(() => AutoLooting()); // ����������� ���������� (await ���������� ������, ������� ����� �������� ����������)
    }

    public void AutoLooting() // ������ ���, ������� ����� ����������� � ��������� ������
    {
        while (true)
        {
            cash += AutoLootingCash;
            Debug.Log($"{cash}");
            Thread.Sleep(1000); // ������� 1000 �� = 1 ���
        }

    }

    public async void FillLblCashAsync() // �� �� ����� �������, ������ �� ��������������
    {
        var progress = new Progress<string>(s => lblCash.text = s); // �����, ����� �� ��� ���������� � lblCash � ������ ������, ��������� �� ���������
        await Task.Run(() => FillLblCash(progress));
    }

    public void FillLblCash(IProgress<string> progress)
    {
        while (true)
        {
            //progress.Report($"<color='#E5E5E5'>{cash.ToString()}</color>");
            progress.Report(cash.ToString());
            Thread.Sleep(200);
        }

    }
}
