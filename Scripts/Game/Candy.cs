using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Candy : MonoBehaviour
{
    [SerializeField] private GameObject[] _candys;

    [SerializeField] private List<Image> _firstCandyBalls;
    [SerializeField] private List<Image> _secondCandyBalls;
    [SerializeField] private List<Image> _thirdCandyBalls;

    [SerializeField] private Sprite[] _sprites;

    private List<int> _items = new List<int>();

    public void Initialize(int count, List<int> items)
    {
        foreach (var item in items) 
        {
            if(item != 3)
                _items.Add(item);
        }
        foreach (var item in _candys)
            item.SetActive(false);
        switch (count)
        {
            case 2:
                {
                    for (int i = 0; i < _firstCandyBalls.Count; i++)
                        _firstCandyBalls[i].sprite = _sprites[_items[i]];
                    _candys[0].SetActive(true);
                    break;
                }
            case 3: 
                {
                    for (int i = 0; i < _secondCandyBalls.Count; i++)
                        _secondCandyBalls[i].sprite = _sprites[_items[i]];
                    _candys[1].SetActive(true);
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < _thirdCandyBalls.Count; i++)
                        _thirdCandyBalls[i].sprite = _sprites[_items[i]];
                    _candys[2].SetActive(true);
                    break;
                }
        }
    }
}
